using BLL.Interfaces;
using BLL.Interfaces.Cache;
using System.Threading.Channels;

namespace DAL.CacheAllocation.Producers
{
    public class ChannelConsumer : IChannelConsumer<AuctionStreamModel>
    {
        IChannelContext<AuctionStreamModel> channelContext;
        Channel<AuctionStreamModel> channel;

        public ChannelConsumer(IChannelContext<AuctionStreamModel> channelContext)
        {
            this.channelContext = channelContext;
            this.channel = channelContext.GetChannel();
        }

        public Task<AuctionStreamModel> Read()
        {
            return channel.Reader.ReadAsync().AsTask();
        }

        public Task<bool> WaitToRead()
        {
            return channel.Reader.WaitToReadAsync().AsTask();
        }
    }
}
