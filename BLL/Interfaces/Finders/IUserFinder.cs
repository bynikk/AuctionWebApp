using BLL.Entities;

namespace BLL.Interfaces.Finders
{
    /// <summary>Provide find operation in database Users collection.</summary>
    public interface IUserFinder
    {
        public Task<User?> GetById(int id);
        public Task<User?> GetByUsername(string username);
    }
}
