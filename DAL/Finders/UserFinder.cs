using BLL.Entities;
using BLL.Interfaces.Database;
using BLL.Interfaces.Finders;
using MongoDB.Driver;

namespace DAL.Findres
{
    /// <summary>Class for finding user.</summary>
    public class UserFinder : IUserFinder
    {
        IDbContext dbContext;
        public UserFinder(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>Gets the user by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<User>? GetById(int id)
        {
            var filter = Builders<User>.Filter.Eq("id", id);

            return dbContext.Users.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>Gets the user by username.</summary>
        /// <param name="username">The username.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<User>? GetByUsername(string username)
        {
            var filter = Builders<User>.Filter.Eq("Username", username);

            return dbContext.Users.Find(filter).FirstOrDefaultAsync();
        }
    }
}
