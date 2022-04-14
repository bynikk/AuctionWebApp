using BLL.Entities;
using BLL.Interfaces;
using MongoDB.Driver;

namespace DAL.Repositories
{
    public class AuctionItemRepository : IRepository<AuctionItem>
    {
        IDbContext context;

        public AuctionItemRepository(IDbContext context)
        {
            this.context = context;
        }

        public Task Create(AuctionItem item)
        {
            return context.AuctionItems.InsertOneAsync(item);
        }

        public Task Delete(int id)
        {
            return context.AuctionItems.DeleteOneAsync(c => c.Id == id);
        }

        public Task<List<AuctionItem>> GetAll()
        {
            return context.AuctionItems.Find(_ => true).ToListAsync(); ;
        }

        public Task Update(AuctionItem item)
        {
            var filter = Builders<AuctionItem>.Filter.Eq("Id", item.Id);
            var update = Builders<AuctionItem>.Update
                                          .Set(x => x.Name, item.Name)
                                          .Set(x => x.CurrentPrice, item.CurrentPrice)
                                          .Set(x => x.StartPrice, item.StartPrice)
                                          .Set(x => x.Time, item.Time);

            return context.AuctionItems.UpdateOneAsync(filter, update);
        }
    }
}
