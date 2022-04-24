using BLL.Interfaces;
using System.Threading.Channels;

namespace DAL.CacheAllocation.Producers
{
    public class ChannelProducer : IChannelProducer<AuctionStreamModel>
    {
        IChannelContext<AuctionStreamModel> channelContext;
        Channel<AuctionStreamModel> channel;
        public ChannelProducer(IChannelContext<AuctionStreamModel> channelContext)
        {
            this.channelContext = channelContext;
            this.channel = channelContext.GetChannel();
        }

        public Task Write(AuctionStreamModel item)
        {
            return channel.Writer.WriteAsync(item).AsTask();
        }
    }
}
