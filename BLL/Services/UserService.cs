using BLL.Entities;
using BLL.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IRepository<User> repository;

        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public Task Create(User cat)
        {
            return repository.Create(cat);
        }

        public Task<List<User>> Get()
        {
            return repository.GetAll();
        }

        public Task Update(User auctionItem)
        {
            return repository.Update(auctionItem);
        }

        public Task Delete(int id)
        {
            return repository.Delete(id);
        }
    }
}
