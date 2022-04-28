using AuctionWebApp.Models;
using AutoMapper;
using BLL.Entities;
using BLL.Interfaces.Finders;
using BLL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuctionWebApp.Controllers
{
    public class AuctionController : Controller
    {
        private readonly ILogger<AuctionController> logger;
        IMapper mapper;
        IAuctionItemService auctionItemService;
        IAuctionItemFinder auctionItemFinder;

        public AuctionController(
            ILogger<AuctionController> logger,
            IMapper mapper,
            IAuctionItemService auctionItemService,
            IAuctionItemFinder auctionItemFinder
            )
        {
            this.auctionItemFinder = auctionItemFinder;
            this.logger = logger;
            this.mapper = mapper;
            this.auctionItemService = auctionItemService;

        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(mapper.Map<List<AuctionItem>, List<AuctionItemViewModel>>(await auctionItemService.Get()));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View("Create");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(AuctionItemViewModel auctionItemViewModel)
        {
            if (!ModelState.IsValid) return View(auctionItemViewModel);
                
            await auctionItemService.Create(mapper.Map<AuctionItemViewModel, AuctionItem>(auctionItemViewModel));
            return View("Create");
        }

        [Authorize]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("/{id}")]
        [Authorize]
        public async Task<IActionResult> AuctionItem(int id)
        {
            var a = mapper.Map<AuctionItem, AuctionItemViewModel>(await auctionItemFinder.GetById(id));
            return View(a);
        }
    }
}