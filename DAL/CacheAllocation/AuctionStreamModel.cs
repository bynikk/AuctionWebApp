using BLL.Entities;

namespace DAL.CacheAllocation
{
    public class AuctionStreamModel
    {
        public string Command { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Owner { get; set; }
        public int StartPrice { get; set; }
        public int CurrentPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? LastBitTime { get; set; }
        public bool OnLive { get; set; }
        public bool OnWait { get; set; }

    }
}
