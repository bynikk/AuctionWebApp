using MongoDB.Bson.Serialization.Attributes;

namespace BLL.Entities
{
    /// <summary>User instance.</summary>
    public class User
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Username")]
        public string UserName { get; set; }
        [BsonElement("Password")]
        public byte[] Password { get; set; }
        [BsonElement("RoleName")]
        public string RoleName { get; set; }

    }
}
