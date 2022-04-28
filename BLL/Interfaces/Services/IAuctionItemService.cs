using BLL.Entities;

namespace BLL.Interfaces.Services
{
    /// <summary>Provide crud operations of auction items collection.</summary>
    public interface IAuctionItemService
    {
        Task Create(AuctionItem auctionItem);
        Task Update(AuctionItem auctionItem);
        Task Delete(int id);
        Task<List<AuctionItem>> Get();
    }
}
