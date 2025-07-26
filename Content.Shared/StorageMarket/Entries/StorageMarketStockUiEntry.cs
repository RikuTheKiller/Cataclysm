using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.Entries;

[NetSerializable, Serializable]
public sealed class StorageMarketStockUiEntry(StorageMarketStockEntry entry, int basePrice, int quantity, bool isCraftable) : IComparable<StorageMarketStockUiEntry>
{
    [ViewVariables]
    public ProtoId<StorageEntryPrototype> Prototype = entry.Prototype;

    [ViewVariables]
    public int IdealStockCount = entry.IdealStockCount;

    [ViewVariables]
    public float MinPriceMultiplier = entry.MinPriceMultiplier;

    [ViewVariables]
    public float MaxPriceMultiplier = entry.MaxPriceMultiplier;

    [ViewVariables]
    public int BasePrice = basePrice;

    [ViewVariables]
    public int Quantity = quantity;

    [ViewVariables]
    public bool IsCraftable = isCraftable;

    public int CompareTo(StorageMarketStockUiEntry? other)
    {
        return string.Compare(ToString(), other?.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return Prototype.ToString();
    }
}