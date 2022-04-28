using BLL.Entities;
using MongoDB.Driver;

namespace BLL.Interfaces.Database
{
    /// <summary>Mongo database context class.</summary>
    public interface IDbContext
    {
        /// <summary>Gets the auction items.</summary>
        IMongoCollection<AuctionItem> AuctionItems { get; }

        /// <summary>Gets the users.</summary>
        IMongoCollection<User> Users { get; }
    }
}
