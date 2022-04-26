using BLL.Entities;
using BLL.Interfaces.Database;
using BLL.Interfaces.Finders;
using MongoDB.Driver;

namespace DAL.Findres
{
    public class UserFinder : IUserFinder
    {
        IDbContext dbContext;
        public UserFinder(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<User>? GetById(int id)
        {
            var filter = Builders<User>.Filter.Eq("id", id);

            return dbContext.Users.Find(filter).FirstOrDefaultAsync();
        }

        public Task<User>? GetByUsername(string username)
        {
            var filter = Builders<User>.Filter.Eq("Username", username);

            return dbContext.Users.Find(filter).FirstOrDefaultAsync();
        }
    }
}
