using BLL.Entities;

namespace BLL.Interfaces.Finders
{
    /// <summary>Provide find operation in database AuctionItems collection.</summary>
    public interface IAuctionItemFinder
    {
        public Task<AuctionItem>? GetById(int id);
        public Task<AuctionItem>? GetByName(string name);
        public Task<AuctionItem>? GetByStartTime(DateTime startTime);
        public Task<AuctionItem>? GetElementReadyToLive();
        public Task<AuctionItem>? GetElementReadyToEnded();
    }
}
