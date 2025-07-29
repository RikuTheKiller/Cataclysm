using Content.Shared.StorageMarket.Data;
using Content.Shared.StorageMarket.Entries;
using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.BUI;

[NetSerializable, Serializable]
public sealed class StorageMarketComputerInterfaceState : BoundUserInterfaceState
{
    /// <summary>
    /// The market entries available for trade on the connected storage net.
    /// </summary>
    public Dictionary<EntProtoId, StorageMarketStockUiEntry> Stock = new();

    /// <summary>
    /// The market entries currently in the buy cart.
    /// </summary>
    public List<StorageMarketBuyCartUiEntry> BuyCart = new();

    /// <summary>
    /// The market entries currently in the sell cart.
    /// </summary>
    public List<StorageMarketSellCartUiEntry> SellCart = new();

    /// <summary>
    /// The tab currently visible in the market menu.
    /// </summary>
    public StorageMarketMenuTab Tab = StorageMarketMenuTab.Default;

    public StorageMarketComputerInterfaceState() { }

    public StorageMarketComputerInterfaceState(StorageMarketComputerInterfaceState copyFrom)
    {
        Stock = copyFrom.Stock;
        BuyCart = copyFrom.BuyCart;
        SellCart = copyFrom.SellCart;
        Tab = copyFrom.Tab;
    }
}