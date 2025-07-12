using System.Diagnostics.CodeAnalysis;
using Content.Server.NodeContainer;
using Content.Server.Power.EntitySystems;
using Content.Server.StorageSys.Events;
using Content.Server.StorageSys.NodeGroups;
using Robust.Server.Containers;

namespace Content.Server.StorageSys.EntitySystems;

public sealed partial class StorageNetSystem : EntitySystem
{
    [Dependency] private readonly PowerReceiverSystem _powerReceiverSystem = default!;
    [Dependency] private readonly ContainerSystem _containerSystem = default!;
    [Dependency] private readonly SharedAppearanceSystem _sharedAppearanceSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        InitializeControllers();
        InitializeContainers();
    }

    /// <summary>
    /// Tries to get the StorageNet connected to the entity.
    /// </summary>
    public bool TryGetStorageNet(EntityUid uid, [NotNullWhen(true)] out StorageNet? net)
    {
        net = null;

        if (!TryComp<NodeContainerComponent>(uid, out var nodeContainer))
            return false;
        if (!nodeContainer.Nodes.TryGetValue("storage", out var node))
            return false;
        if (node.NodeGroup is not StorageNet)
            return false;

        net = (StorageNet)node.NodeGroup;
        return true;
    }

    /// <summary>
    /// Wrapper for 'RaiseLocalEvent(uid, args)'
    /// </summary>
    public void RaiseStorageNetLoadNodeEvent(EntityUid uid, StorageNetLoadNodeEvent args)
    {
        RaiseLocalEvent(uid, args);
    }

    /// <summary>
    /// Wrapper for 'RaiseLocalEvent(uid, args)'
    /// </summary>
    public void RaiseStorageNetRemoveNodeEvent(EntityUid uid, StorageNetRemoveNodeEvent args)
    {
        RaiseLocalEvent(uid, args);
    }
}