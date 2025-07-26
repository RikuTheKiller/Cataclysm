using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.Entries;

[NetSerializable, Serializable]
public sealed class StorageMarketSellCartEntry(EntProtoId prototype, int quantity)
{
    [ViewVariables]
    public EntProtoId Prototype = prototype;

    [ViewVariables]
    public int Quantity = quantity;
}