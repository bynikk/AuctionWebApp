using System.Threading.Channels;

namespace BLL.Interfaces
{
    public interface IChannelContext<T> where T : class
    {
        public Channel<T> GetChannel();
    }
}
