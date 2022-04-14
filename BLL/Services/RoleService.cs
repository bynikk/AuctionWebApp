using BLL.Entities;
using BLL.Interfaces;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        IRepository<Role> repository;

        public RoleService(IRepository<Role> repository)
        {
            this.repository = repository;
        }

        public Task Create(Role cat)
        {
            return repository.Create(cat);
        }

        public Task<List<Role>> Get()
        {
            return repository.GetAll();
        }

        public Task Update(Role auctionItem)
        {
            return repository.Update(auctionItem);
        }

        public Task Delete(int id)
        {
            return repository.Delete(id);
        }
    }
}
