using Content.Shared.StorageMarket.Data;

namespace Content.Server.StorageSys.Components;

[RegisterComponent]
public sealed partial class StorageMarketComputerComponent : Component
{
    /// <summary>
    /// The maximum distance from sell pallets and crate machines.
    /// </summary>
    [ViewVariables, DataField]
    public int MachineRange = 3;

    /// <summary>
    /// The market entries available for trade on the connected storage net.
    /// </summary>
    [ViewVariables]
    public List<StorageMarketEntry> Entries = new();

    /// <summary>
    /// The market entries currently in the buy cart.
    /// </summary>
    [ViewVariables]
    public List<StorageMarketEntry> BuyCart = new();

    /// <summary>
    /// The market entries currently in the sell cart.
    /// </summary>
    [ViewVariables]
    public List<StorageMarketEntry> SellCart = new();
}