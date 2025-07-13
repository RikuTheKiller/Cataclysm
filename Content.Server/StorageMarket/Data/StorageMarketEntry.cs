using Content.Shared.Stacks;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageMarket.Data;

public sealed class StorageMarketEntry
{
    [ViewVariables]
    public EntProtoId? Prototype;

    [ViewVariables]
    public ProtoId<StackPrototype>? StackPrototype;

    [ViewVariables]
    public double Price;

    [ViewVariables]
    public StorageMarketCategories Categories;

    [ViewVariables]
    public StorageMarketDepartments Departments;

    public StorageMarketEntry(EntProtoId prototype, double price, StorageMarketCategories categories, StorageMarketDepartments departments)
    {
        Prototype = prototype;
        StackPrototype = null;
        Price = price;
        Categories = categories;
        Departments = departments;
    }

    public StorageMarketEntry(ProtoId<StackPrototype> stackPrototype, double price, StorageMarketCategories categories, StorageMarketDepartments departments)
    {
        Prototype = null;
        StackPrototype = stackPrototype;
        Price = price;
        Categories = categories;
        Departments = departments;
    }
}