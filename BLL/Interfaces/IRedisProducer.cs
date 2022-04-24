using BLL.Entities;

namespace BLL.Interfaces.Cache
{
    public interface IRedisProducer<T> where T : class
    {
        public void AddInsertCommand(T item);
        public void AddDeleteCommand(int key);
    }
}
