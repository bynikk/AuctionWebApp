using MongoDB.Bson.Serialization.Attributes;

namespace BLL.Entities
{
    public class Role
    {
        [BsonElement("UserId")]
        public int UserId { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        
    }
}
