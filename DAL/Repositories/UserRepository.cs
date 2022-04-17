using BLL.Entities;
using BLL.Interfaces;
using MongoDB.Driver;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        IDbContext context;
        public UserRepository(IDbContext context)
        {
            this.context = context;
        }
        public Task Create(User item)
        {
            return context.Users.InsertOneAsync(item);
        }


        // REFACTOPR THIS
        public Task Delete(int id)
        {
            return context.Users.DeleteOneAsync(c => c.Id == id);
        }

        public Task<List<User>> GetAll()
        {
            return context.Users.Find(_ => true).ToListAsync(); ;
        }

        public Task Update(User item)
        {
            var filter = Builders<User>.Filter.Eq("Id", item.Id);
            var update = Builders<User>.Update
                                          .Set(x => x.Name, item.Name)
                                          .Set(x => x.UserName, item.UserName)
                                          .Set(x => x.Password, item.Password)
                                          .Set(x => x.RoleName, item.RoleName);

            return context.Users.UpdateOneAsync(filter, update);
        }
    }
}
