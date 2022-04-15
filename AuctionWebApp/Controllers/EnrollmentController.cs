using AutoMapper;
using BLL;
using BLL.Entities;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionWebApp.Controllers
{
    public class EnrollmentController : Controller
    {
        IMapper mapper;
        IUserService userService;
        IUserFinder userFinder;
        public EnrollmentController(
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

            userService.Create(mapper.Map<UserViewModel, User>(model));

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
            return RedirectToAction("Users", "Admin");

        }

        // cookie
    }
}
