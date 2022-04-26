using BLL.Entities;

namespace BLL.Interfaces.Finders
{
    public interface IUserFinder
    {
        public Task<User?> GetById(int id);
        public Task<User?> GetByUsername(string username);
    }
}
