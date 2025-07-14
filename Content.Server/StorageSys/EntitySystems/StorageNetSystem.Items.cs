using Content.Server.StorageSys.Components;
using Content.Server.StorageSys.Events;
using Content.Server.StorageSys.NodeGroups;
using Content.Shared.Item;
using Content.Shared.Stacks;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageSys.EntitySystems;

public sealed partial class StorageNetSystem : EntitySystem
{
    public void InitializeItems()
    {
        SubscribeLocalEvent<ItemStorageContainerComponent, StorageNetLoadNodeEvent>(OnItemStorageContainerLoadNode);
        SubscribeLocalEvent<ItemStorageContainerComponent, StorageNetRemoveNodeEvent>(OnItemStorageContainerRemoveNode);
    }

    public void OnItemStorageContainerLoadNode(EntityUid uid, ItemStorageContainerComponent comp, StorageNetLoadNodeEvent args)
    {
        args.Net.ItemContainers.Add(uid);
    }

    public void OnItemStorageContainerRemoveNode(EntityUid uid, ItemStorageContainerComponent comp, StorageNetRemoveNodeEvent args)
    {
        args.Net.ItemContainers.Remove(uid);
    }

    public int GetItemCount(EntProtoId item, StorageNet net)
    {
        var total = 0;

        foreach (var containerUid in net.ItemContainers)
            total += GetItemCount(item, containerUid);

        return total;
    }

    public int GetItemCount(EntProtoId item, EntityUid containerUid, ItemStorageContainerComponent? container = null)
    {
        if (!Resolve(containerUid, ref container))
            return 0;

        return container.Storage.GetValueOrDefault(item);
    }

    public int TryChangeItemCount(EntProtoId item, int amount, StorageNet net)
    {
        var remainder = amount;

        foreach (var containerUid in net.MaterialContainers)
        {
            remainder -= TryChangeItemCount(item, remainder, containerUid);

            if (remainder == 0)
                return amount;
        }

        return amount - remainder;
    }

    public int TryChangeItemCount(EntProtoId item, int amount, EntityUid containerUid, ItemStorageContainerComponent? container = null)
    {
        if (!Resolve(containerUid, ref container))
            return 0;
        if (!_prototypeManager.TryIndex<EntityPrototype>(item, out var itemPrototype))
            return 0;
        if (!itemPrototype.TryGetComponent<ItemComponent>(out var itemComponent, _componentFactory))
            return 0;
        if (!_prototypeManager.TryIndex(itemComponent.Size, out var sizePrototype))
            return 0;

        var existingAmount = container.Storage.GetValueOrDefault(item);
        var finalAmount = Math.Clamp(existingAmount + amount, 0, container.Capacity / sizePrototype.Weight);

        container.Storage[item] = finalAmount;

        return finalAmount - existingAmount;
    }

    public int GetItemStackCount(ProtoId<StackPrototype> stack, StorageNet net)
    {
        var total = 0;

        foreach (var containerUid in net.ItemContainers)
            total += GetItemStackCount(stack, containerUid);

        return total;
    }

    public int GetItemStackCount(ProtoId<StackPrototype> stack, EntityUid containerUid, ItemStorageContainerComponent? container = null)
    {
        if (!Resolve(containerUid, ref container))
            return 0;

        return container.StackStorage.GetValueOrDefault(stack);
    }

    public int TryChangeItemStackCount(ProtoId<StackPrototype> stack, int amount, StorageNet net)
    {
        var remainder = amount;

        foreach (var containerUid in net.MaterialContainers)
        {
            remainder -= TryChangeItemStackCount(stack, remainder, containerUid);

            if (remainder == 0)
                return amount;
        }

        return amount - remainder;
    }

    public int TryChangeItemStackCount(ProtoId<StackPrototype> stack, int amount, EntityUid containerUid, ItemStorageContainerComponent? container = null)
    {
        if (!Resolve(containerUid, ref container))
            return 0;
        if (!_prototypeManager.TryIndex<EntityPrototype>(stack, out var stackPrototype))
            return 0;
        if (!stackPrototype.TryGetComponent<ItemComponent>(out var itemComponent, _componentFactory))
            return 0;
        if (!stackPrototype.TryGetComponent<StackComponent>(out var stackComponent, _componentFactory))
            return 0;
        if (!_prototypeManager.TryIndex(itemComponent.Size, out var sizePrototype))
            return 0;

        var existingAmount = container.StackStorage.GetValueOrDefault(stack);
        var finalAmount = Math.Clamp(existingAmount + amount, 0, container.Capacity * _sharedStackSystem.GetMaxCount(stackComponent) / sizePrototype.Weight);

        container.StackStorage[stack] = finalAmount;

        return finalAmount - existingAmount;
    }
}