using AuctionWebApp.Models;
using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuctionWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        IMapper mapper;
        IAuctionItemService auctionItemService;

        public HomeController(
            ILogger<HomeController> logger,
            IMapper mapper,
            IAuctionItemService auctionItemService
            )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.auctionItemService = auctionItemService;
        }

        public async Task<IActionResult> Index()
        {
            return View(mapper.Map<List<AuctionItem>, List<AuctionItemViewModel>>(await auctionItemService.Get()));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuctionItemViewModel auctionItemViewModel)
        {
            if (ModelState.IsValid) await auctionItemService.Create(mapper.Map<AuctionItemViewModel, AuctionItem>(auctionItemViewModel));
            return View("Create");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}