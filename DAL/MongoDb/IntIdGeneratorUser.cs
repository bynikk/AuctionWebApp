using BLL.Entities;
using BLL.Interfaces.Database;
using MongoDB.Driver;

namespace DAL
{
    /// <summary>Provide next avalible int identifier for new element.</summary>
    public class IntIdGeneratorUser : IIntIdGenerator<User>
    {
        /// <summary>Generates the identifier.</summary>
        /// <param name="container">The container.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public int GenerateId(IMongoCollection<User> container)
        {
            var col = (IMongoCollection<User>)container;
            var sortBy = col.Find(x => x.Id != null).SortByDescending(e => e.Id);
            var last = sortBy.FirstOrDefault();
            var id = (last == null) ? 1 : (int)last.Id + 1;
            return id;
        }

        /// <summary>Determines whether the specified identifier is empty.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified identifier is empty; otherwise, <c>false</c>.</returns>
        public bool IsEmpty(object id)
        {
            return ((id is int) && (id as int? == 0));
        }
    }
}
