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
        IMapper mapper;
        IAuctionItemService auctionItemService;
        IAuctionItemFinder auctionItemFinder;

        public AuctionController(
            IMapper mapper,
            IAuctionItemService auctionItemService,
            IAuctionItemFinder auctionItemFinder
            )
        {
            this.auctionItemFinder = auctionItemFinder;
            this.mapper = mapper;
            this.auctionItemService = auctionItemService;

        }

        //[Authorize]
        /// <summary>Provide list of all auction items.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
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

        /// <summary>Creates the specified auction item by view model.</summary>
        /// <param name="auctionItemViewModel">The auction item view model from UI.</param>
        /// <returns>
        ///   <br />
        /// </returns>
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


        /// <summary>Return view of instance of auction item by id.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Route("/{id}")]
        [Authorize]
        public async Task<IActionResult> AuctionItem(int id)
        {
            var a = mapper.Map<AuctionItem, AuctionItemViewModel>(await auctionItemFinder.GetById(id));
            return View(a);
        }
    }
}