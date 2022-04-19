using BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace AuctionWebApp.Hubs
{
    public class AuctionHub : Hub
    {
        IAuctionItemFinder auctionItemFinder;
        IAuctionItemService auctionItemService;

        public AuctionHub(
            IAuctionItemFinder auctionItemFinder,
            IAuctionItemService auctionItemService)
        {
            this.auctionItemFinder = auctionItemFinder;
            this.auctionItemService = auctionItemService;
        }

        public async Task Bit(string bit, string id)
        {
            // threading.channel
            var item = await auctionItemFinder.GetById(int.Parse(id));
            item.CurrentPrice += int.Parse(bit);
            item.LastBitTime = DateTime.UtcNow.AddMinutes(5);

            await auctionItemService.Update(item);
            //
            await this.Clients.All.SendAsync("ReceiveCurrPrice", item.CurrentPrice, id);
            await this.Clients.All.SendAsync("ReceiveBitTime", item.LastBitTime, id);
        }
    }
}
