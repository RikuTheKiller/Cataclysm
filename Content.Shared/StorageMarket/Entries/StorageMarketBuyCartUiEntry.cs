using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.Entries;

[NetSerializable, Serializable]
public sealed class StorageMarketBuyCartUiEntry(ProtoId<StorageEntryPrototype> prototype, int quantity)
{
    [ViewVariables]
    public ProtoId<StorageEntryPrototype> Prototype = prototype;

    [ViewVariables]
    public int Quantity = quantity;
}