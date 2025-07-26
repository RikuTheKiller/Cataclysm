using Content.Shared.StorageMarket.Data;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared.StorageMarket.Prototypes;

[Prototype]
public sealed partial class StorageEntryPrototype : IPrototype
{
    [ViewVariables, IdDataField]
    public string ID { get; private set; } = default!;

    [DataField(required: true)]
    public EntProtoId EntityPrototype { get; private set; }

    [DataField(required: true, customTypeSerializer: typeof(FlagSerializer<StorageMarketCategory>))]
    public int Categories { get; private set; }

    [DataField(required: true, customTypeSerializer: typeof(FlagSerializer<StorageMarketDepartment>))]
    public int Departments { get; private set; }
}