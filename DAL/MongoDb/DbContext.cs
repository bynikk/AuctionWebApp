using BLL.Entities;
using BLL.Interfaces;
using DAL.Config;
using MongoDB.Driver;

namespace DAL.MongoDb
{
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
        public IMongoCollection<User> Users => database.GetCollection<User>(mongoConfig.UsersCollectionName);
        public IMongoCollection<AuctionItem> AuctionItems => database.GetCollection<AuctionItem>(mongoConfig.AuctionItemsCollectionName);
    }
}
