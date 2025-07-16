using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageMarket.Data;

public sealed class StorageMarketData
{
    public List<ProtoId<StorageEntryPrototype>> Entries;

    public StorageMarketData()
    {
        Entries = new();
    }

    public StorageMarketData(StorageMarketData copyFrom)
    {
        Entries = new(copyFrom.Entries);
    }
}