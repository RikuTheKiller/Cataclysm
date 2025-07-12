using Content.Shared.Whitelist;

namespace Content.Server.StorageSys.Components;

[RegisterComponent]
public sealed partial class StorageContainerComponent : Component
{
    public const string ContainerName = "storage_container";

    [DataField]
    public int Capacity = 1000;
}