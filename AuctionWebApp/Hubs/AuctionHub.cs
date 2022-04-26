using BLL.Interfaces.Finders;
using BLL.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace AuctionWebApp.Hubs
{
    public class AuctionHub : Hub
    {
        IAuctionItemFinder auctionItemFinder;
        IAuctionItemService auctionItemService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuctionHub(
            IAuctionItemFinder auctionItemFinder,
            IAuctionItemService auctionItemService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.auctionItemFinder = auctionItemFinder;
            this.auctionItemService = auctionItemService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public CancellationToken Token { get; set; }

        public async Task Bit(string bit, string id)
        {
            var item = await auctionItemFinder.GetById(int.Parse(id));

            if (!item.OnWait && item.OnLive)
            {
                item.CurrentPrice += int.Parse(bit);
                item.LastBitTime = DateTime.UtcNow.AddSeconds(30);
                item.Owner = httpContextAccessor.HttpContext.User.Identity.Name.ToString();
                await auctionItemService.Update(item);
                //
                await this.Clients.All.SendAsync("ReceiveBitData", item.CurrentPrice, item.LastBitTime, item.Owner, id);
            }
            else
            {
                // bad request
                return;
            }
        }

        public async Task StatusRequest(string id)
        {
            var date = DateTime.UtcNow;
            var item = await auctionItemFinder.GetById(int.Parse(id));

            if (!item.OnWait && item.OnLive &&
                item.LastBitTime != null && date >= item.LastBitTime)
            {
                // end
                item.OnLive = false;
                item.OnWait = false;
                await auctionItemService.Update(item);
                await this.Clients.All.SendAsync("ReceiveAuctionEndData", id);
            }
            else if (item.OnWait && !item.OnLive &&
                     date >= item.StartTime)
            {
                // go live
                item.OnLive = true;
                item.OnWait = false;
                await auctionItemService.Update(item);
                await this.Clients.All.SendAsync("ReceiveAuctionLiveData", id);
            }
            else
            {
                // bad request
                return;
            }

        }
    }
}
