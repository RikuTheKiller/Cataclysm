using Content.Shared.Stacks;
using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.Data;

[NetSerializable, Serializable]
public readonly struct StorageMarketEntry(StorageEntryPrototype prototype, int basePrice, int quantity, bool isCraftable) : IComparable<StorageMarketEntry>
{
    [ViewVariables]
    public readonly EntProtoId? Prototype = prototype.Prototype;

    [ViewVariables]
    public readonly ProtoId<StackPrototype>? StackPrototype = prototype.StackPrototype;

    [ViewVariables]
    public readonly StorageMarketCategories Categories = prototype.Categories;

    [ViewVariables]
    public readonly StorageMarketDepartments Departments = prototype.Departments;

    [ViewVariables]
    public readonly int BasePrice = basePrice;

    [ViewVariables]
    public readonly int Quantity = quantity;

    [ViewVariables]
    public readonly bool IsCraftable = isCraftable;

    public int CompareTo(StorageMarketEntry other)
    {
        return string.Compare(ToString(), other.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        if (Prototype != null)
            return Prototype.Value.ToString();
        if (StackPrototype != null)
            return StackPrototype.Value.ToString();

        return string.Empty;
    }
}