using BLL.Entities;
using BLL.Interfaces.Database;
using BLL.Interfaces.Repositories;
using MongoDB.Driver;

namespace DAL.Repositories
{
    /// <summary>Provide crud operation in database users collection.</summary>
    public class UserRepository : IRepository<User>
    {
        IDbContext context;
        /// <summary>Initializes a new instance of the <see cref="UserRepository" /> class.</summary>
        /// <param name="context">The database context.</param>
        public UserRepository(IDbContext context)
        {
            this.context = context;
        }
        /// <summary>Creates the specified item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task Create(User item)
        {
            return context.Users.InsertOneAsync(item);
        }

        /// <summary>Deletes the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task Delete(int id)
        {
            return context.Users.DeleteOneAsync(c => c.Id == id);
        }

        /// <summary>Gets all.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<List<User>> GetAll()
        {
            return context.Users.Find(_ => true).ToListAsync();
        }

        /// <summary>Updates the specified item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <br />
        /// </returns>
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
