namespace BLL.Interfaces.Repositories
{
    /// <summary>Provide crud operation in database T instance of collection.</summary>
    /// <typeparam name="T">Collection instance type.</typeparam>
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);
    }
}