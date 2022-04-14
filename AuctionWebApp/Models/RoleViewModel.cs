using MongoDB.Bson.Serialization.Attributes;

namespace BLL.Entities
{
    public class RoleViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        
    }
}
