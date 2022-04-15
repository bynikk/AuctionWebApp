using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionWebApp.Controllers
{
    public class AdminController : Controller
    {
        IMapper mapper;
        IUserService userService;
        IRoleService roleService;
        public AdminController(IUserService userService, IRoleService roleService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.roleService = roleService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Users()
        {
            return View(mapper.Map<List<User>,List<UserViewModel>>(await userService.Get()));
        }
        public async Task<IActionResult> Roles()
        {
            return View(mapper.Map<List<Role>, List<RoleViewModel>>(await roleService.Get()));
        }
        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            await roleService.Create(mapper.Map<RoleViewModel, Role>(model));
            return View();
        }
    }
}
