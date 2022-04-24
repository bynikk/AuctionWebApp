using MongoDB.Bson.Serialization.Attributes;

namespace BLL.Entities
{
    public class AuctionItem
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Owner")]
        public string? Owner { get; set; }
        [BsonElement("StartPrice")]
        public int StartPrice { get; set; }
        [BsonElement("CurrentPrice")]
        public int CurrentPrice { get; set; }
        [BsonElement("StartTime")]
        public DateTime StartTime { get; set; }
        [BsonElement("LastBitTime")]
        public DateTime? LastBitTime { get; set; }
        [BsonElement("OnLive")]
        public bool OnLive { get; set; }
        [BsonElement("OnWait")]
        public bool OnWait { get; set; }
        public AuctionItem()
        {
            OnWait = true;
            OnLive = false;
        }
    }
}
