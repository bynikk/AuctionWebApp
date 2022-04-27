using BLL.Entities;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IRepository<User> repository;

        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        /// <summary>Creates the specified item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task Create(User item)
        {
            return repository.Create(item);
        }

        /// <summary>Gets all users from mongo collection.</summary>
        /// <returns>The collection items.</returns>
        public Task<List<User>> Get()
        {
            return repository.GetAll();
        }

        /// <summary>Updates specified auction item.</summary>
        /// <param name="auctionItem">The auction item.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task Update(User item)
        {
            return repository.Update(item);
        }

        /// <summary>Deletes the User by specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task Delete(int id)
        {
            return repository.Delete(id);
        }
    }
}
