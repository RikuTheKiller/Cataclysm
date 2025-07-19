using Content.Shared.StorageMarket.Data;
using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageSys.Components;

[RegisterComponent]
public sealed partial class StorageMarketComputerComponent : Component
{
    /// <summary>
    /// The maximum distance from sell pallets and crate machines.
    /// </summary>
    [ViewVariables, DataField]
    public int MachineRange = 3;
}