using BLL.Entities;
using BLL.Interfaces;
using MongoDB.Driver;

namespace DAL.Finders
{
    public class AuctionItemFinder : IAuctionItemFinder
    {
        IDbContext dbContext;
        public AuctionItemFinder(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<AuctionItem>? GetById(int id)
        {
            var filter = Builders<AuctionItem>.Filter.Eq("_id", id);

            return dbContext.AuctionItems.Find(filter).FirstOrDefaultAsync();
        }

        public Task<AuctionItem>? GetByName(string name)
        {
            var filter = Builders<AuctionItem>.Filter.Eq("Name", name);

            return dbContext.AuctionItems.Find(filter).FirstOrDefaultAsync();
        }
        public Task<AuctionItem>? GetByStartTime(DateTime startTime)
        {
            var filter = Builders<AuctionItem>.Filter.Lte("StartTime", startTime);

            return dbContext.AuctionItems.Find(filter).FirstOrDefaultAsync();
        }

        public Task<AuctionItem>? GetElementReadyToLive()
        {
            return dbContext.AuctionItems.Find(x => 
                                            x.StartTime <= DateTime.UtcNow &&
                                            x.OnLive == false).FirstOrDefaultAsync();
        }
    }
}
