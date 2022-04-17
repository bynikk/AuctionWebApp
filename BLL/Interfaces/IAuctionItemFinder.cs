using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IAuctionItemFinder
    {
        public Task<AuctionItem?> GetById(int id);
        public Task<AuctionItem?> GetByUsername(string name);
    }
}
