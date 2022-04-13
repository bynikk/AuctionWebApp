using MongoDB.Bson.Serialization.Attributes;

namespace BLL.Entities
{
    public class AuctionItem
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("StartPrice")]
        public int StartPrice { get; set; }
        [BsonElement("CurrentPrice")]
        public int CurrentPrice { get; set; }
        [BsonElement("Time")]
        public DateTime Time { get; set; }
    }
}
