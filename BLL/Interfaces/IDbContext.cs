using BLL.Entities;
using MongoDB.Driver;

namespace BLL.Interfaces
{
    public interface IDbContext
    {
        IMongoCollection<AuctionItem> AuctionItems { get; }
        IMongoCollection<User> Users { get; }
        IMongoCollection<Role> Roles { get; }
    }
}
