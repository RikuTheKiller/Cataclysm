using Content.Shared.StorageMarket.Data;
using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.BUI;

[NetSerializable, Serializable]
public sealed class StorageMarketComputerInterfaceState(List<StorageMarketEntry> entries, List<StorageMarketEntry> buyCart, List<StorageMarketEntry> sellCart) : BoundUserInterfaceState
{
    /// <summary>
    /// The market entries available for trade on the connected storage net.
    /// </summary>
    public List<StorageMarketEntry> Entries = entries;

    /// <summary>
    /// The market entries currently in the buy cart.
    /// </summary>
    public List<StorageMarketEntry> BuyCart = buyCart;

    /// <summary>
    /// The market entries currently in the sell cart.
    /// </summary>
    public List<StorageMarketEntry> SellCart = sellCart;
}