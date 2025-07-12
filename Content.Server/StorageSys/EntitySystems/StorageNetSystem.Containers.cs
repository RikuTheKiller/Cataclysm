using System.Diagnostics.CodeAnalysis;
using Content.Server.StorageSys.Components;
using Content.Server.StorageSys.Events;
using Content.Server.StorageSys.NodeGroups;
using Content.Shared.Item;

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

    public bool TryInsertItem(EntityUid itemUid, StorageNet net)
    {
        if (!TryComp<ItemComponent>(itemUid, out var item))
            return false;

        foreach (var containerUid in net.Containers)
        {
            if (!_powerReceiverSystem.IsPowered(containerUid))
                continue;
            if (TryInsertItem(itemUid, containerUid))
                return true;
        }

        return false;
    }

    private bool TryInsertItem(EntityUid itemUid, EntityUid containerUid)
    {
        if (!_containerSystem.TryGetContainer(containerUid, StorageContainerComponent.ContainerName, out var container))
            return false;

        return _containerSystem.Insert(itemUid, container);
    }
}