using BLL.Entities;
using BLL.Interfaces;
using MongoDB.Driver;

namespace DAL.Finders
{
    public class RoleFinder : IRoleFinder
    {
        IDbContext dbContext;
        public RoleFinder(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<Role?> GetById(int id)
        {
            var filter = Builders<Role>.Filter.Eq("UserId", id);

            return dbContext.Roles.Find(filter).FirstOrDefaultAsync();
        }

        public Task<Role?> GetByName(string name)
        {
            var filter = Builders<Role>.Filter.Eq("Name", name);

            return dbContext.Roles.Find(filter).FirstOrDefaultAsync();
        }
    }
}
