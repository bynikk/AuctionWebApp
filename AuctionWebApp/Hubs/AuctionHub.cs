using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
            await auctionItemService.Update(item);
            //
            await this.Clients.All.SendAsync("ReceiveBit",  bit, id);
            await this.Clients.All.SendAsync("ReceiveСurrPrice",  item.CurrentPrice, id);
        }
    }
}
