using BLL.Entities;
using BLL.Interfaces.Database;
using BLL.Interfaces.Finders;
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
                                            DateTime.UtcNow >= x.StartTime &&
                                            x.OnLive == false &&
                                            x.OnWait == true).FirstOrDefaultAsync();
        }
        public Task<AuctionItem>? GetElementReadyToEnded()
        {
            return dbContext.AuctionItems.Find(x =>
                                            DateTime.UtcNow >= x.LastBitTime &&
                                            x.OnWait == false &&
                                            x.OnLive == true).FirstOrDefaultAsync();
        }
    }
}
