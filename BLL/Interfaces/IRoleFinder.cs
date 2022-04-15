using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IRoleFinder
    {
        public Task<Role?> GetById(int id);
        public Task<Role?> GetByName(string name);
    }
}
