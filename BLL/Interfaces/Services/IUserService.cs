using BLL.Entities;

namespace BLL.Interfaces.Services
{
    /// <summary>Provide crud operations of user collection.</summary>
    public interface IUserService
    {
        Task Create(User auctionItem);
        Task Update(User auctionItem);
        Task Delete(int id);
        Task<List<User>> Get();
    }
}
