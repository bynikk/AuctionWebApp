using BLL.Entities;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;

namespace BLL.Services
{
    public class AuctionItemService : IAuctionItemService
    {
        IRepository<AuctionItem> repository;

        public AuctionItemService(IRepository<AuctionItem> repository)
        {
            this.repository = repository;
        }

        /// <summary>Creates the specified cat.</summary>
        /// <param name="cat">The cat.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task Create(AuctionItem cat)
        {
            return repository.Create(cat);
        }

        /// <summary>Gets all auction items from db collection.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<List<AuctionItem>> Get()
        {
            return repository.GetAll();
        }

        /// <summary>Updates the specified auction item.</summary>
        /// <param name="auctionItem">The auction item.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task Update(AuctionItem auctionItem)
        {
            return repository.Update(auctionItem);
        }

        /// <summary>Deletes the auction item by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task Delete(int id)
        {
            return repository.Delete(id);
        }
    }
}
