﻿using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionWebApp.Controllers
{
    public class AdminController : Controller
    {
        IMapper mapper;
        IUserService userService;
        public AdminController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Users()
        {
            return View(mapper.Map<List<User>,List<UserViewModel>>(await userService.Get()));
        }
    }
}