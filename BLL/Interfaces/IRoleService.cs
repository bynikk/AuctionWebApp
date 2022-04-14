using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IRoleService
    {
        Task Create(Role auctionItem);
        Task Update(Role auctionItem);
        Task Delete(int id);
        Task<List<Role>> Get();
    }
}
