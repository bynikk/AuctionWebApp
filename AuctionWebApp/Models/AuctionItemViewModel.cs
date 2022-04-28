namespace AuctionWebApp.Models
{

    /// <summary>View model of class Auction Item.</summary>
    public class AuctionItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Owner { get; set; }

        public int StartPrice { get; set; }

        public int CurrentPrice { get; set; }

        /// <summary>Gets or sets the start time. Time when of auction item start auction.</summary>
        /// <value>The start time.</value>
        public DateTime StartTime { get; set; }

        public DateTime? LastBitTime { get; set; }

        /// <summary>Gets or sets a value indicating whether [auction item on live].</summary>
        /// <value>
        ///   <c>true</c> if [on live]; otherwise, <c>false</c>.</value>
        public bool OnLive { get; set; }

        /// <summary>Gets or sets a value indicating whether [auction item on wait].</summary>
        /// <value>
        ///   <c>true</c> if [on wait]; otherwise, <c>false</c>.</value>
        public bool OnWait { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AuctionItemViewModel" /> class. Sets a indicating values which set that item "OnWait" status.</summary>
        public AuctionItemViewModel()
        {
            OnWait = true;
            OnLive = false;
        }
    }
}
