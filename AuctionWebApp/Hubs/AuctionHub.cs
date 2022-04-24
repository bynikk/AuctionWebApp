using BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

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
            // threading.channel
            var item = await auctionItemFinder.GetById(int.Parse(id));

            if (!item.OnWait && item.OnLive)
            {
                item.CurrentPrice += int.Parse(bit);
                item.LastBitTime = DateTime.UtcNow.AddMinutes(5);
                item.Owner = httpContextAccessor.HttpContext.User.Identity.Name.ToString();
                await auctionItemService.Update(item);
                //
                await this.Clients.All.SendAsync("ReceiveBitData", item.CurrentPrice, item.LastBitTime, item.Owner, id);
            }
            else
            {
                return;
            }
        }
    }
}
