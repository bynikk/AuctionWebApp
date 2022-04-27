using BLL.Entities;
using BLL.Interfaces.Database;
using BLL.Interfaces.Repositories;
using MongoDB.Driver;

namespace DAL.Repositories
{
    public class AuctionItemRepository : IRepository<AuctionItem>
    {
        IDbContext context;

        /// <summary>Initializes a new instance of the <see cref="AuctionItemRepository" /> class.</summary>
        /// <param name="context">The database context.</param>
        public AuctionItemRepository(IDbContext context)
        {
            this.context = context;
        }

        /// <summary>Creates the specified item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task Create(AuctionItem item)
        {
            if (item.CurrentPrice == 0) item.CurrentPrice = item.StartPrice;
            return context.AuctionItems.InsertOneAsync(item);
        }

        /// <summary>Deletes the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task Delete(int id)
        {
            return context.AuctionItems.DeleteOneAsync(c => c.Id == id);
        }

        /// <summary>Gets all.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<List<AuctionItem>> GetAll()
        {
            return context.AuctionItems.Find(_ => true).ToListAsync();
        }

        /// <summary>Updates the specified item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task Update(AuctionItem item)
        {
            var filter = Builders<AuctionItem>.Filter.Eq("Id", item.Id);
            var update = Builders<AuctionItem>.Update
                                          .Set(x => x.Name, item.Name)
                                          .Set(x => x.CurrentPrice, item.CurrentPrice)
                                          .Set(x => x.StartPrice, item.StartPrice)
                                          .Set(x => x.StartTime, item.StartTime)
                                          .Set(x => x.Owner, item.Owner)
                                          .Set(x => x.OnLive, item.OnLive)
                                          .Set(x => x.OnWait, item.OnWait)
                                          .Set(x => x.LastBitTime, item.LastBitTime);

            return context.AuctionItems.UpdateOneAsync(filter, update);
        }
    }
}
