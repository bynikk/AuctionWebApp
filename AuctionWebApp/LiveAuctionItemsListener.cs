using AuctionWebApp.Hubs;
using BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DAL.Live
{
    public class LiveAuctionItemsListener : ILiveAuctionItemsListner
    {
        CancellationTokenSource tokenSource;
        IAuctionItemFinder auctionItemFinder;
        IAuctionItemService auctionItemService;
        IHubContext<AuctionHub> hubContext;


        public LiveAuctionItemsListener(
            IServiceProvider provider,
            IHubContext<AuctionHub> hubContext)
        {
            this.hubContext = hubContext;
            using (var scope = provider.CreateScope())
            {
                auctionItemFinder = scope.ServiceProvider.GetRequiredService<IAuctionItemFinder>();
                auctionItemService = scope.ServiceProvider.GetRequiredService<IAuctionItemService>();
            }
            tokenSource = new CancellationTokenSource();
            Token = tokenSource.Token;
        }

        public CancellationToken Token { get; set; }

        public async void Listen()
        {
            while (!Token.IsCancellationRequested)
            {
                // add lambda fuction
                var item = await auctionItemFinder.GetElementReadyToLive();
                if (item != null)
                {
                    lock (auctionItemService)
                    {
                        item.OnLive = true;
                        item.OnWait = false;
                        auctionItemService.Update(item);
                        hubContext.Clients.All.SendAsync("ReceiveAuctionLiveData", item.Id);
                    }
                }
            }
        }
    }
}
