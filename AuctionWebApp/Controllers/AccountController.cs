using AutoMapper;
using BLL;
using BLL.Entities;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuctionWebApp.Controllers
{
    public class AccountController : Controller
    {
        IMapper mapper;
        IUserService userService;
        IUserFinder userFinder;
        public AccountController(
            IUserService userService,
            IMapper mapper,
            IUserFinder userFinder)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.userFinder = userFinder;
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: EnrollmentController/SignUp
        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            User? user = await userFinder.GetByUsername(model.UserName);
            
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "username already exist");
                return View(model);
            }

            bool result = await CreateUser(model, RoleNames.User);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "bad request");
                return View(model);
            }

            return View();
        }

        public ActionResult LoginIn()
        {
            return View();
        }

        // POST: EnrollmentController/LoginIn
        [HttpPost]
        public async Task<IActionResult> LoginIn(UserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            User? user = await userFinder.GetByUsername(model.UserName);
            if (user == null)
            {
                return View(model);

            }
            if (user.Password.Equals(model.Password.GetHash()))
            {
                return View(model);
            }
            await Authenticate(user);
            return RedirectToAction("Users", "Admin");

        }

        // cookie
        private async Task<bool> CreateUser(UserViewModel userViewModel, string rolename)
        {
            userViewModel.RoleName = rolename;
            userService.Create(mapper.Map<UserViewModel, User>(userViewModel));
            return true;
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName),
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.RedirectToAction("Index", "Home");
        }
    }
}
