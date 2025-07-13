using Content.Server.StorageMarket.Data;

namespace Content.Server.StorageSys.Data;

public sealed class StorageControllerData
{
    public StorageMarketData MarketData;

    public StorageControllerData()
    {
        MarketData = new();
    }

    public StorageControllerData(StorageMarketData marketData)
    {
        MarketData = marketData;
    }

    public StorageControllerData Copy()
    {
        return new(MarketData.Copy());
    }
}