using BLL.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //[BsonId(IdGenerator = typeof(IntIdGenerator<Customer>))]
    public class IntIdGenerator<T> : IIdGenerator
    {
        public object GenerateId(object container, object document)
        {
            var col = (IMongoCollection<AuctionItem>)container;
            var sortBy = col.Find(x => x.Id == x.Id).SortByDescending(e => e.Id);
            var last = sortBy.FirstOrDefault();
            var id = (last == null) ? 1 : (int)last.Id + 1;
            return id;
        }

        public bool IsEmpty(object id)
        {
            return ((id is int) && (id as int? == 0));
        }
    }
}
