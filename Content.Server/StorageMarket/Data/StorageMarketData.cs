namespace Content.Server.StorageMarket.Data;

public sealed class StorageMarketData
{
    public List<StorageMarketEntry> Entries;

    public StorageMarketData()
    {
        Entries = [];
    }

    public StorageMarketData(List<StorageMarketEntry> entries)
    {
        Entries = entries;
    }

    public StorageMarketData Copy()
    {
        return new([.. Entries]);
    }
}