using Content.Server.StorageSys.Components;
using Content.Server.StorageSys.Events;

namespace Content.Server.StorageSys.EntitySystems;

public sealed partial class StorageNetSystem : EntitySystem
{
    public void InitializeContainers()
    {
        SubscribeLocalEvent<StorageContainerComponent, StorageNetLoadNodeEvent>(OnStorageContainerLoadNode);
        SubscribeLocalEvent<StorageContainerComponent, StorageNetRemoveNodeEvent>(OnStorageContainerRemoveNode);
    }

    public void OnStorageContainerLoadNode(EntityUid uid, StorageContainerComponent comp, StorageNetLoadNodeEvent args)
    {
        args.Net.Containers.Add(uid);
    }

    public void OnStorageContainerRemoveNode(EntityUid uid, StorageContainerComponent comp, StorageNetRemoveNodeEvent args)
    {
        args.Net.Containers.Remove(uid);
    }
}