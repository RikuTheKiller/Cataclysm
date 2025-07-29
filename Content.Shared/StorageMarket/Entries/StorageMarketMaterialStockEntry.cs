using Content.Shared.Materials;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.Entries;

[NetSerializable, Serializable]
public sealed class StorageMarketMaterialStockEntry(ProtoId<MaterialPrototype> prototype, int idealStockCount, float minPriceMultiplier, float maxPriceMultiplier) : IComparable<StorageMarketMaterialStockEntry>
{
    [ViewVariables]
    public ProtoId<MaterialPrototype> Prototype = prototype;

    [ViewVariables]
    public int IdealStockCount = idealStockCount;

    [ViewVariables]
    public float MinPriceMultiplier = minPriceMultiplier;

    [ViewVariables]
    public float MaxPriceMultiplier = maxPriceMultiplier;

    public int CompareTo(StorageMarketMaterialStockEntry? other)
    {
        return string.Compare(ToString(), other?.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return Prototype.ToString();
    }
}