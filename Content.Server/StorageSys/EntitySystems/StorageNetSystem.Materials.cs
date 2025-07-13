using Content.Server.StorageSys.Components;
using Content.Server.StorageSys.Events;
using Content.Server.StorageSys.NodeGroups;
using Content.Shared.Materials;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageSys.EntitySystems;

public sealed partial class StorageNetSystem : EntitySystem
{
    public void InitializeMaterials()
    {
        SubscribeLocalEvent<MaterialStorageContainerComponent, StorageNetLoadNodeEvent>(OnMaterialContainerLoadNode);
        SubscribeLocalEvent<MaterialStorageContainerComponent, StorageNetRemoveNodeEvent>(OnMaterialContainerRemoveNode);
    }

    public void OnMaterialContainerLoadNode(EntityUid uid, MaterialStorageContainerComponent comp, StorageNetLoadNodeEvent args)
    {
        args.Net.MaterialContainers.Add(uid);
    }

    public void OnMaterialContainerRemoveNode(EntityUid uid, MaterialStorageContainerComponent comp, StorageNetRemoveNodeEvent args)
    {
        args.Net.MaterialContainers.Remove(uid);
    }

    /// <summary>
    /// Returns the amount of the material in the storage net.
    /// </summary>
    public int GetMaterial(ProtoId<MaterialPrototype> material, StorageNet net)
    {
        var amount = 0;

        foreach (var containerUid in net.MaterialContainers)
            amount += GetMaterial(material, containerUid);

        return amount;
    }

    /// <summary>
    /// Returns the amount of the material in the material storage container.
    /// </summary>
    public int GetMaterial(ProtoId<MaterialPrototype> material, EntityUid containerUid, MaterialStorageContainerComponent? container = null)
    {
        if (!Resolve(containerUid, ref container))
            return 0;

        return container.Storage.GetValueOrDefault(material);
    }

    /// <summary>
    /// Returns the calculated final change in the amount of the material in the storage net, without actually changing it.
    /// </summary>
    public int GetMaxMaterialChange(ProtoId<MaterialPrototype> material, int amount, StorageNet net)
    {
        var remainder = amount;

        foreach (var containerUid in net.MaterialContainers)
        {
            remainder -= GetMaxMaterialChange(material, remainder, containerUid);

            if (remainder == 0)
                return amount;
        }

        return amount - remainder;
    }

    /// <summary>
    /// Returns the calculated final change in the amount of the material in the container, without actually changing it.
    /// </summary>
    public int GetMaxMaterialChange(ProtoId<MaterialPrototype> material, int amount, EntityUid containerUid, MaterialStorageContainerComponent? container = null)
    {
        if (!Resolve(containerUid, ref container))
            return 0;

        var existingAmount = container.Storage.GetValueOrDefault(material);
        var finalAmount = Math.Clamp(existingAmount + amount, 0, container.Capacity);

        return finalAmount - existingAmount;
    }

    /// <summary>
    /// Tries to change the amount of the material in the storage net by the given amount.
    /// </summary>
    /// <returns>The final change in the amount of the material.</returns>
    public int TryChangeMaterial(ProtoId<MaterialPrototype> material, int amount, StorageNet net)
    {
        var remainder = amount;

        foreach (var containerUid in net.MaterialContainers)
        {
            remainder -= TryChangeMaterial(material, remainder, containerUid);

            if (remainder == 0)
                return amount;
        }

        return amount - remainder;
    }

    /// <summary>
    /// Tries to change the amount of the material in the container by the given amount.
    /// </summary>
    /// <returns>The final change in the amount of the material.</returns>
    public int TryChangeMaterial(ProtoId<MaterialPrototype> material, int amount, EntityUid containerUid, MaterialStorageContainerComponent? container = null)
    {
        if (!Resolve(containerUid, ref container))
            return 0;

        var existingAmount = container.Storage.GetValueOrDefault(material);
        var finalAmount = Math.Clamp(existingAmount + amount, 0, container.Capacity);

        container.Storage[material] = finalAmount;

        return finalAmount - existingAmount;
    }
}