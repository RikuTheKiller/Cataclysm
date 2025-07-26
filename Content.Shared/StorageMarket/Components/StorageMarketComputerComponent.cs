using Content.Shared.StorageMarket.EntitySystems;
using Robust.Shared.GameStates;

namespace Content.Shared.StorageMarket.Components;

[RegisterComponent, NetworkedComponent]
public sealed partial class StorageMarketComputerComponent : Component
{
    /// <summary>
    /// The maximum distance from sell pallets and crate machines.
    /// </summary>
    [ViewVariables, DataField]
    public int MachineRange = 3;
}