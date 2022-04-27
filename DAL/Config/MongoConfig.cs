namespace DAL.Config;

/// <summary>Class for mongo configuration.</summary>
public class MongoConfig
{
    public string Ip { get; set; }
    public int Port { get; set; }
    public string DatabaseName { get; set; }
    public string UsersCollectionName { get; set; }
    public string AuctionItemsCollectionName { get; set; }
    public string ConnectionString
    {
        get
        {
            return $@"mongodb://{Ip}:{Port}";
        }
    }
}
