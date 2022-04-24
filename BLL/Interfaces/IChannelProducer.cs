namespace BLL.Interfaces
{
    public interface IChannelProducer<T> where T : class
    {
        public Task Write(T item);
    }
}
