using BLL.Entities;
using BLL.Interfaces;

namespace BLL.Services
{
    public class AuctionItemService : IAuctionItemService
    {
        IRepository<AuctionItem> repository;

        public AuctionItemService(IRepository<AuctionItem> repository)
        {
            this.repository = repository;
        }

        public Task Create(AuctionItem cat)
        {
            return repository.Create(cat);
        }

        public Task<List<AuctionItem>> Get()
        {
            return repository.GetAll();
        }

        public Task Update(AuctionItem auctionItem)
        {
            return repository.Update(auctionItem);
        }

        public Task Delete(int id)
        {
            return repository.Delete(id);
        }
    }
}
