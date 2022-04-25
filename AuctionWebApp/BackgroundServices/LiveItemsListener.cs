using AuctionWebApp.Hubs;
using BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace AuctionWebApp.BackgroundServices;

public class LiveItemsListener : BackgroundService
{
    CancellationTokenSource tokenSource;
    IAuctionItemFinder auctionItemFinder;
    IAuctionItemService auctionItemService;
    IHubContext<AuctionHub> hubContext;

    public LiveItemsListener(
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

                item.OnLive = true;
                item.OnWait = false;
                await auctionItemService.Update(item);
                await hubContext.Clients.All.SendAsync("ReceiveAuctionLiveData", item.Id);

            }
            await Task.Delay(1000);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!Token.IsCancellationRequested)
        {
            // add lambda fuction
            var item = await auctionItemFinder.GetElementReadyToLive();
            if (item != null)
            {

                item.OnLive = true;
                item.OnWait = false;
                await auctionItemService.Update(item);
                await hubContext.Clients.All.SendAsync("ReceiveAuctionLiveData", item.Id);

            }
            await Task.Delay(1000);
        }
    }

}
