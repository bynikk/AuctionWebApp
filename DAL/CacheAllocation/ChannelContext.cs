using BLL.Interfaces;
using System.Threading.Channels;

namespace DAL.CacheAllocation
{
    public class ChannelContext : IChannelContext<AuctionStreamModel>
    {
        Channel<AuctionStreamModel> channel;

        public ChannelContext()
        {
            channel = Channel.CreateUnbounded<AuctionStreamModel>();
        }

        public Channel<AuctionStreamModel> GetChannel()
        {
            return channel;
        }
    }
}
