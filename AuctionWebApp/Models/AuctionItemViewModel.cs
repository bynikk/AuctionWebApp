namespace AuctionWebApp.Models
{
    public class AuctionItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartPrice { get; set; }
        public int CurrentPrice { get; set; }
        public DateTime Time { get; set; }

        public AuctionItemViewModel()
        {
            this.Time = DateTime.Now;
        }
    }
}
