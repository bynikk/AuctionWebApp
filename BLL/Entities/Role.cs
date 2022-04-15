using MongoDB.Bson.Serialization.Attributes;

namespace BLL.Entities
{
    public class Role
    {
        [BsonElement("Id")]
        public int Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        
    }
}
