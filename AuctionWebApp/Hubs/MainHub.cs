using BLL.Interfaces.Finders;
using BLL.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace AuctionWebApp.Hubs
{
    public class MainHub : Hub
    {
        IAuctionItemFinder auctionItemFinder;
        IAuctionItemService auctionItemService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public MainHub(
            IAuctionItemFinder auctionItemFinder,
            IAuctionItemService auctionItemService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.auctionItemFinder = auctionItemFinder;
            this.auctionItemService = auctionItemService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public CancellationToken Token { get; set; }

        public async Task ItemStatusRequest(string id)
        {
            Console.WriteLine(id);
            //var date = DateTime.UtcNow;
            //var item = await auctionItemFinder.GetById(int.Parse(id));

            //if (!item.OnWait && item.OnLive &&
            //    item.LastBitTime != null && date >= item.LastBitTime)
            //{
            //    // end
            //    item.OnLive = false;
            //    item.OnWait = false;
            //    await auctionItemService.Update(item);
            //    await this.Clients.All.SendAsync("ReceiveAuctionEndData", id);
            //}
            //else if (item.OnWait && !item.OnLive &&
            //         date >= item.StartTime)
            //{
            //    // go live
            //    item.OnLive = true;
            //    item.OnWait = false;
            //    await auctionItemService.Update(item);
            //    await this.Clients.All.SendAsync("ReceiveAuctionLiveData", id);
            //}
            //else
            //{
            //    // bad request
            //    return;
            //}

        }
    }
}
