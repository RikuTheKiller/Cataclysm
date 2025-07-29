using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.Entries;

[NetSerializable, Serializable]
public sealed class StorageMarketSellCartUiEntry(EntProtoId prototype, int basePrice, int quantity)
{
    [ViewVariables]
    public EntProtoId Prototype = prototype;

    [ViewVariables]
    public int BasePrice = basePrice;

    [ViewVariables]
    public int Quantity = quantity;
}