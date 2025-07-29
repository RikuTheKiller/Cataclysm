using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.Entries;

[NetSerializable, Serializable]
public sealed class StorageMarketItemStockEntry(ProtoId<StorageEntryPrototype> prototype, int idealStockCount, float minPriceMultiplier, float maxPriceMultiplier) : IComparable<StorageMarketItemStockEntry>
{
    [ViewVariables]
    public ProtoId<StorageEntryPrototype> Prototype = prototype;

    [ViewVariables]
    public int IdealStockCount = idealStockCount;

    [ViewVariables]
    public float MinPriceMultiplier = minPriceMultiplier;

    [ViewVariables]
    public float MaxPriceMultiplier = maxPriceMultiplier;

    public int CompareTo(StorageMarketItemStockEntry? other)
    {
        return string.Compare(ToString(), other?.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return Prototype.ToString();
    }
}