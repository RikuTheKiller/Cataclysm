using Content.Shared.StorageMarket.Data;
using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageMarket.Data;

public sealed class StorageMarketData
{
    public Dictionary<ProtoId<StorageEntryPrototype>, StorageMarketStockEntry> Stock;

    public StorageMarketData()
    {
        Stock = new();
    }

    public StorageMarketData(StorageMarketData copyFrom)
    {
        Stock = new(copyFrom.Stock);
    }
}