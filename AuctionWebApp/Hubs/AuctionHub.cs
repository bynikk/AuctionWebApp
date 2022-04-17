using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AuctionWebApp.Hubs
{
    public class AuctionHub : Hub
    {
        [Authorize]
        public async Task Send(string message)
        {
            //this.Context.User.Claims.First().Value;
            await this.Clients.All.SendAsync("Send", message);
        }
    }
}
