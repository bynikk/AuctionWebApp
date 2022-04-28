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
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(UserViewModel model)
         {
            if (!ModelState.IsValid) return View(model);

            User? user = await _userFinder.GetByUsername(model.UserName);
            
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "username already exist");
                return View(model);
            }

            user = _mapper.Map<UserViewModel, User>(model);

            try
            {
                await _userService.Create(user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"bad request {ex.Message}");
                return View(model);
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> LoginIn(UserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            User? user = await _userFinder.GetByUsername(model.UserName);
            if (user == null)
            {
                return View(model);

            }

            if (!user.Password.SequenceEqual(model.Password.GetHash()))
            {
                ModelState.AddModelError("505", "incorrect password");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName),
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

            return RedirectToAction("Index", "Auction");

        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.RedirectToAction("Index", "Home");
        }
    }
}
