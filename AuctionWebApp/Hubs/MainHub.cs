using BLL.Interfaces.Finders;
using BLL.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace AuctionWebApp.Hubs
{
    /// <summary>Handle SignalR requests from client-side at index auction page.</summary>
    public class MainHub : Hub
    {
        IAuctionItemFinder auctionItemFinder;
        IAuctionItemService auctionItemService;

        public MainHub(
            IAuctionItemFinder auctionItemFinder,
            IAuctionItemService auctionItemService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.auctionItemFinder = auctionItemFinder;
            this.auctionItemService = auctionItemService;
        }

        /// <summary>Set timer and status at UI by handing item by id.</summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public async Task ItemStatusRequest(string id)
        {
            try
            { 
                var item = await auctionItemFinder.GetById(int.Parse(id));

                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                var date = DateTime.UtcNow;

                if (item.OnWait && !item.OnLive)
                {
                    // wait
                    await this.Clients.All.SendAsync("ReceiveItemTimer", item.Id, "OnWait", item.StartTime);
                }
                else if(!item.OnWait && !item.OnLive)
                {
                    // end
                    await this.Clients.All.SendAsync("ReceiveItemTimer", item.Id, "Ended", DateTime.Now.ToString("O"));
                }
                else if (!item.OnWait && item.OnLive &&
                         item.LastBitTime != null)
                {
                    // live
                    await this.Clients.All.SendAsync("ReceiveItemTimer", item.Id, "OnLive", item.LastBitTime?.ToString("O"));
                }
                else if (!item.OnWait && item.OnLive
                         && item.LastBitTime == null)
                {
                    // live [wait for first bid]
                    await this.Clients.All.SendAsync("ReceiveItemTimer", item.Id, "Waiting first bid", DateTime.Now.ToString("O"));
                }
                else
                {
                    // bad request
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

        }

        /// <summary>Update status property at server-side.</summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public async Task ItemStatusUpdateRequest(string id)
        {
            try
            {
                var item = await auctionItemFinder.GetById(int.Parse(id));

                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                var date = DateTime.UtcNow;

                if (date >= item.LastBitTime)
                {
                    // end
                    item.OnLive = false;
                    item.OnWait = false;
                    await auctionItemService.Update(item);
                }
                else if (date >= item.StartTime)
                {
                    // live
                    item.OnLive = true;
                    item.OnWait = false;
                    await auctionItemService.Update(item);
                }
                else
                {
                    item.OnLive = false;
                    item.OnWait = true;
                    await auctionItemService.Update(item);
                }

                await ItemStatusRequest(item.Id.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

        }
    }
}
