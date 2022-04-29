using MongoDB.Bson.Serialization.Attributes;

namespace BLL.Entities
{
    /// <summary>Auction item instance which represent the lot abstraction.</summary>
    public class AuctionItem
    {
        public int Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Owner")]
        public string? Owner { get; set; }

        [BsonElement("StartPrice")]
        public int StartPrice { get; set; }

        [BsonElement("CurrentPrice")]
        public int CurrentPrice { get; set; }

        /// <summary>Gets or sets the start time. Time when of auction item start auction.</summary>
        /// <value>The start time.</value>
        [BsonElement("StartTime")]
        public DateTime StartTime { get; set; }

        [BsonElement("LastBitTime")]
        public DateTime? LastBitTime { get; set; }

        /// <summary>Gets or sets a value indicating whether [auction item on live].</summary>
        /// <value>
        ///   <c>true</c> if [on live]; otherwise, <c>false</c>.</value>
        [BsonElement("OnLive")]
        public bool OnLive { get; set; }

        /// <summary>Gets or sets a value indicating whether [auction item on wait].</summary>
        /// <value>
        ///   <c>true</c> if [on wait]; otherwise, <c>false</c>.</value>
        [BsonElement("OnWait")]
        public bool OnWait { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AuctionItem" /> class. Sets a indicating values which set that item "OnWait" status.</summary>
        public AuctionItem()
        {
            OnWait = true;
            OnLive = false;
        }
    }
}
