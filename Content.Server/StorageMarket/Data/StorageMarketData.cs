using Content.Shared.Materials;
using Content.Shared.StorageMarket.Entries;
using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageMarket.Data;

public sealed class StorageMarketData
{
    [ViewVariables]
    public Dictionary<ProtoId<StorageEntryPrototype>, StorageMarketItemStockEntry> Stock;

    [ViewVariables]
    public Dictionary<ProtoId<MaterialPrototype>, StorageMarketMaterialStockEntry> MaterialStock;

    public StorageMarketData()
    {
        Stock = new();
        MaterialStock = new();
    }

    public StorageMarketData(StorageMarketData copyFrom)
    {
        Stock = new(copyFrom.Stock);
        MaterialStock = new(copyFrom.MaterialStock);
    }
}