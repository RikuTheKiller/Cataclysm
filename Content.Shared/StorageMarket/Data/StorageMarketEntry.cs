using Content.Shared.Stacks;
using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.Data;

[NetSerializable, Serializable]
public sealed class StorageMarketEntry(StorageEntryPrototype prototype, int basePrice, int quantity, bool isCraftable) : IComparable<StorageMarketEntry>
{
    [ViewVariables]
    public EntProtoId? Prototype = prototype.Prototype;

    [ViewVariables]
    public ProtoId<StackPrototype>? StackPrototype = prototype.StackPrototype;

    [ViewVariables]
    public StorageMarketCategories Categories = prototype.Categories;

    [ViewVariables]
    public StorageMarketDepartments Departments = prototype.Departments;

    [ViewVariables]
    public int BasePrice = basePrice;

    [ViewVariables]
    public int Quantity = quantity;

    [ViewVariables]
    public bool IsCraftable = isCraftable;

    public int CompareTo(StorageMarketEntry? other)
    {
        return string.Compare(ToString(), other?.ToString(), StringComparison.OrdinalIgnoreCase);
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