using BLL.Entities;
using BLL.Interfaces.Database;
using BLL.Interfaces.Finders;
using MongoDB.Driver;

namespace DAL.Finders
{
    /// <summary>Class for finding auction items.</summary>
    public class AuctionItemFinder : IAuctionItemFinder
    {
        IDbContext dbContext;
        public AuctionItemFinder(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>Gets the AuctionItem by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<AuctionItem>? GetById(int id)
        {
            var filter = Builders<AuctionItem>.Filter.Eq("_id", id);

            return dbContext.AuctionItems.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        ///   <para>
        /// Gets the AuctionItem by name.</para>
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<AuctionItem>? GetByName(string name)
        {
            var filter = Builders<AuctionItem>.Filter.Eq("Name", name);

            return dbContext.AuctionItems.Find(filter).FirstOrDefaultAsync();
        }
        /// <summary>Gets the AuctionItem by start time.</summary>
        /// <param name="startTime">The start time.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<AuctionItem>? GetByStartTime(DateTime startTime)
        {
            var filter = Builders<AuctionItem>.Filter.Lte("StartTime", startTime);

            return dbContext.AuctionItems.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>Gets the element ready to live.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<AuctionItem>? GetElementReadyToLive()
        {
            return dbContext.AuctionItems.Find(x => 
                                            DateTime.UtcNow >= x.StartTime &&
                                            x.OnLive == false &&
                                            x.OnWait == true).FirstOrDefaultAsync();
        }
        /// <summary>Gets the element ready to ended.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<AuctionItem>? GetElementReadyToEnded()
        {
            return dbContext.AuctionItems.Find(x =>
                                            DateTime.UtcNow >= x.LastBitTime &&
                                            x.OnWait == false &&
                                            x.OnLive == true).FirstOrDefaultAsync();
        }
    }
}
