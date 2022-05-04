using AutoMapper;
using BLL;
using BLL.Entities;
using BLL.Interfaces.Finders;
using BLL.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuctionWebApp.Controllers
{
    public class AccountController : Controller
    {
        IMapper _mapper;
        IUserService _userService;
        IUserFinder _userFinder;
        /// <summary>Initializes a new instance of the <see cref="AccountController" /> class.</summary>
        /// <param name="userService">The user service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userFinder">The user finder.</param>
        public AccountController(
            IUserService userService,
            IMapper mapper,
            IUserFinder userFinder)
        {
            _userService = userService;
            _mapper = mapper;
            _userFinder = userFinder;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: EnrollmentController/SignUp
        /// <summary>Create new user by ViewModel.</summary>
        /// <param name="model">The view model from UI.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="System.ArgumentException">Username already exist.</exception>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(UserViewModel model)
         {
            try
            {
                if (!ModelState.IsValid) return View(model);

                User? user = await _userFinder.GetByUsername(model.UserName);

                if (user != null) throw new ArgumentException("Username already exist.");

                model.RoleName = RoleNames.Admin;
                user = _mapper.Map<UserViewModel, User>(model);
                await _userService.Create(user);

                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("500", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginIn()
        {
            return View();
        }

        /// <summary>Authorize user by cookie creation.</summary>
        /// <param name="model">The view model from UI.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="System.ArgumentException">Invalid {nameof(user.UserName)}
        /// or
        /// Invalid {nameof(model.Password)}</exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> LoginIn(UserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                User? user = await _userFinder.GetByUsername(model.UserName);
                if (user == null) throw new ArgumentException($"Invalid {nameof(user.UserName)}");

                if (!user.Password.SequenceEqual(model.Password.GetHash())) throw new ArgumentException($"Invalid {nameof(model.Password)}");

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName),
                };

                var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                return RedirectToAction("Index", "Auction");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("500", ex.Message);
                return View(model);
            }
        }

        /// <summary>Delete cookie value from session.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.RedirectToAction("Index", "Home");
        }
    }
}
