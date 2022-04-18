using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IUserFinder
    {
        public Task<User?> GetById(int id);
        public Task<User?> GetByUsername(string username);
    }
}
