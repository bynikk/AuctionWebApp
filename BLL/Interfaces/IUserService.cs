using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task Create(User auctionItem);
        Task Update(User auctionItem);
        Task Delete(int id);
        Task<List<User>> Get();
    }
}
