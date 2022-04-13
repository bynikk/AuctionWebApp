using MongoDB.Bson.Serialization.Attributes;

namespace BLL.Entities
{
    public class User
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Username")]
        public string UserName { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("RoleId")]
        public int RoleId { get; set; }

    }
}
