using AuctionWebApp.Hubs;
using BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace AuctionWebApp.BackgroundServices;

public class EndItemsListener : BackgroundService
{
    CancellationTokenSource tokenSource;
    IAuctionItemFinder auctionItemFinder;
    IAuctionItemService auctionItemService;
    IHubContext<AuctionHub> hubContext;


    public EndItemsListener(
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

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!Token.IsCancellationRequested)
        {
            // add lambda fuction
            var item = await auctionItemFinder.GetElementReadyToEnded();
            if (item != null)
            {

                    item.OnLive = false;
                    item.OnWait = false;
                    await auctionItemService.Update(item);
                await hubContext.Clients.All.SendAsync("ReceiveAuctionEndData", item.Id);

            }

            await Task.Delay(1000);
        }
    }
}
