using BLL.Entities;
using MongoDB.Driver;

namespace BLL.Interfaces.Database
{
    public interface IDbContext
    {
        IMongoCollection<AuctionItem> AuctionItems { get; }
        IMongoCollection<User> Users { get; }
    }
}
