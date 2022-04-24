using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Interfaces.Cache;

namespace DAL.CacheAllocation
{
    public class Cache : ICache<AuctionItem>
    {
        private Dictionary<int, WeakReference> cacheDictionary;

        CancellationTokenSource tokenSource;
        CancellationToken Token;

        IRedisProducer<AuctionItem> redisProducer;
        IRedisConsumer redisComsumer;

        IChannelProducer<AuctionStreamModel> channelProducer;
        IChannelConsumer<AuctionStreamModel> channelComsumer;

        IMapper mapper;

        public Cache(
            IRedisProducer<AuctionItem> redisProducer,
            IRedisConsumer redisConsumer,
            IChannelProducer<AuctionStreamModel> channelProducer,
            IChannelConsumer<AuctionStreamModel> channelComsumer,
            IMapper mapper)
        {
            cacheDictionary = new();
            this.redisProducer = redisProducer;
            this.redisComsumer = redisConsumer;

            this.channelProducer = channelProducer;
            this.channelComsumer = channelComsumer;

            this.mapper = mapper;
            tokenSource = new CancellationTokenSource();
            Token = tokenSource.Token;
        }


        public void Set(AuctionItem item)
        { 
            redisProducer.AddInsertCommand(item);
        }

        public AuctionItem? Get(int key)
        { 
            if (cacheDictionary.ContainsKey(key) && cacheDictionary[key].IsAlive)
            {
                return cacheDictionary[key].Target as AuctionItem;
            }
            return null;
        }

        public void Delete(int key)
        {
            redisProducer.AddDeleteCommand(key);
        }

        public async void ListenRedisTask()
        {
            redisComsumer.OnDataReceived += (sender, message) =>
            {
                channelProducer.Write(ParseResult(message));
            };

            await Task.Run(() => redisComsumer.WaitToGetNewElement());
        }


        public async void ListenChannelTask()
        {
            while (!Token.IsCancellationRequested)
            {
                if (await channelComsumer.WaitToRead())
                {
                    var streamItem = await channelComsumer.Read();
                    lock (cacheDictionary)
                    {
                        ExecuteDicionaryCommand(
                            streamItem.Command,
                            mapper.Map<AuctionStreamModel, AuctionItem>(streamItem));
                    }
                }
            }
        }

        private AuctionStreamModel ParseResult(string redisData)
        {
            Console.WriteLine(redisData);
            var dict = redisData.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
           .Select(part => part.Split('='))
           .ToDictionary(split => split[0], split => split[1]);

            var cat = new AuctionStreamModel();
            switch (dict.Count)
            {
                case 4:
                    cat = new AuctionStreamModel
                    {
                        Command = dict[FieldNames.Command],
                        Id = Convert.ToInt32(dict[FieldNames.Id]),
                        Name = dict[FieldNames.Name],
                        Owner = Convert.ToString(dict[FieldNames.Owner]),
                        StartPrice = Convert.ToInt32(dict[FieldNames.StartPrice]),
                        CurrentPrice = Convert.ToInt32(dict[FieldNames.CurrentPrice]),
                        StartTime = Convert.ToDateTime(dict[FieldNames.StartTime]),
                        LastBitTime = Convert.ToDateTime(dict[FieldNames.LastBitTime]),
                        OnLive = Convert.ToBoolean(dict[FieldNames.OnLive]),
                        OnWait = Convert.ToBoolean(dict[FieldNames.OnWait]),

                    };

                    break;
                case 2:
                    cat = new AuctionStreamModel
                    {
                        Command = dict[FieldNames.Command],
                        Id = Convert.ToInt32(dict[FieldNames.Id]),
                    };
                    break;
            } 
            return cat;
        }
        private void ExecuteDicionaryCommand(string dictCommand, AuctionItem item)
        {
            switch (dictCommand)
            {
                case CommandTypes.Insert:
                    Console.WriteLine($"{CommandTypes.Insert} item at id:{item.Id}");
                    cacheDictionary.Add(item.Id, new WeakReference(item));
                    break;
                case CommandTypes.Delete:
                    Console.WriteLine($"{CommandTypes.Delete} item at id:{item.Id}");
                    cacheDictionary.Remove(item.Id);
                    break;
            }
        }
    }
}
