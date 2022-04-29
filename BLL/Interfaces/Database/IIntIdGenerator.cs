using MongoDB.Driver;

namespace BLL.Interfaces.Database
{
    /// <summary>Provide next avalible int identifier for new element.</summary>
    public interface IIntIdGenerator<T> where T : class
    {
        /// <summary>Generates the identifier.</summary>
        /// <param name="container">The container.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public int GenerateId(IMongoCollection<T> container);

        /// <summary>Determines whether the specified identifier is empty.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified identifier is empty; otherwise, <c>false</c>.</returns>
        public bool IsEmpty(object id);
    }
}
