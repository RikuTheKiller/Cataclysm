using Content.Server.StorageMarket.Data;
using Content.Shared.Stacks;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageMarket.Prototypes;

[Prototype]
public sealed partial class StorageMarketEntryPrototype : IPrototype
{
    [ViewVariables, IdDataField]
    public string ID { get; private set; } = default!;

    [DataField]
    public EntProtoId? Prototype { get; private set; }

    [DataField]
    public ProtoId<StackPrototype>? StackPrototype { get; private set; }

    [DataField(required: true)]
    public StorageMarketCategories Categories { get; private set; }

    [DataField(required: true)]
    public StorageMarketDepartments Departments { get; private set; }
}