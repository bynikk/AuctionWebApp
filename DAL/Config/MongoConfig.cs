namespace DAL.Config;

/// <summary>Class for mongo configuration.</summary>
public class MongoConfig
{
    public string Ip { get; set; }
    public int Port { get; set; }
    public string DatabaseName { get; set; }
    public string UsersCollectionName { get; set; }
    public string AuctionItemsCollectionName { get; set; }

    /// <summary>Return conecting string by using class properties.</summary>
    /// <value>The connection string. Format:"mongodb://MongoConfig.Ip:MongoConfig.Port"</value>
    public string ConnectionString
    {
        get
        {
            return $@"mongodb://{Ip}:{Port}";
        }
    }
}
