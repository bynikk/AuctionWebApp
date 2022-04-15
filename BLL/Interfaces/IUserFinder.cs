using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserFinder
    {
        public Task<User?> GetById(int id);
        public Task<User?> GetByUsername(string username);
    }
}
