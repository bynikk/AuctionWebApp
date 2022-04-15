using BLL.Entities;
using BLL.Interfaces;
using MongoDB.Driver;

namespace DAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        IDbContext context;
        public RoleRepository(IDbContext context)
        {
            this.context = context;
        }
        public Task Create(Role item)
        {
            return context.Roles.InsertOneAsync(item);
        }

        public Task Delete(int id)
        {
            return context.Roles.DeleteOneAsync(c => c.Id == id);
        }

        public Task<List<Role>> GetAll()
        {
            return context.Roles.Find(_ => true).ToListAsync(); ;
        }

        public Task Update(Role item)
        {
            var filter = Builders<Role>.Filter.Eq("UserId", item.Id);
            var update = Builders<Role>.Update
                                          .Set(x => x.Name, item.Name);

            return context.Roles.UpdateOneAsync(filter, update);
        }
    }
}
