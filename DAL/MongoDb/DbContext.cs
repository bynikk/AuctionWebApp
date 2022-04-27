using BLL.Entities;
using BLL.Interfaces.Database;
using DAL.Config;
using MongoDB.Driver;

namespace DAL.MongoDb
{
    /// <summary>Mongo database context class.</summary>
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase database;
        MongoConfig mongoConfig;

        public DbContext(MongoConfig config)
        {
            mongoConfig = config;

            var client = new MongoClient(mongoConfig.ConnectionString);
            database = client.GetDatabase(mongoConfig.DatabaseName);
        }

        /// <summary>Gets the users.</summary>
        /// <value>The users collection.</value>
        public IMongoCollection<User> Users => database.GetCollection<User>(mongoConfig.UsersCollectionName);

        /// <summary>Gets the auction items.</summary>
        /// <value>The auction items collection.</value>
        public IMongoCollection<AuctionItem> AuctionItems => database.GetCollection<AuctionItem>(mongoConfig.AuctionItemsCollectionName);
    }
}
