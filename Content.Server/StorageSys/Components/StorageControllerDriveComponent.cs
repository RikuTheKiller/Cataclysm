using Content.Server.StorageSys.Data;
using Content.Server.StorageSys.NodeGroups;

namespace Content.Server.StorageSys.Components;

[RegisterComponent]
public sealed partial class StorageControllerDriveComponent : Component
{
    [ViewVariables]
    public StorageControllerData? Data = new();

    [ViewVariables]
    public StorageNet? ConnectedNet;

    /// <summary>
    /// Whitelist for StorageEntryPrototypes that will automatically have stock requests put up for them.
    /// </summary>
    [DataField]
    public StorageMarketWhitelist? InitialMarketStockWhitelist;
}