using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IAuctionItemFinder
    {
        public Task<AuctionItem>? GetById(int id);
        public Task<AuctionItem>? GetByName(string name);
        public Task<AuctionItem>? GetByStartTime(DateTime startTime);
        public Task<AuctionItem>? GetElementReadyToLive();
    }
}
