using Content.Server._Cataclysm.Storage.Components;
using Content.Server._Cataclysm.Storage.EntitySystems;
using Content.Server._Cataclysm.Storage.Nodes;
using Content.Server.NodeContainer.NodeGroups;
using Content.Server.NodeContainer.Nodes;
using Content.Server.Power.EntitySystems;

namespace Content.Server._Cataclysm.Storage.NodeGroups;

[NodeGroup(NodeGroupID.Storage)]
public sealed partial class StorageNet : BaseNodeGroup
{
    private StorageSystem _storageSystem = default!;

    public readonly List<EntityUid> Controllers = [];

    public override void Initialize(Node sourceNode, IEntityManager entMan)
    {
        base.Initialize(sourceNode, entMan);

        _storageSystem = entMan.System<StorageSystem>();
    }

    public override void LoadNodes(List<Node> groupNodes)
    {
        base.LoadNodes(groupNodes);

        foreach (var node in groupNodes)
            if (node is not IStoragePassiveNode)
                _storageSystem.RaiseStorageNetLoadNodeEvent(node.Owner, new(node, this));
    }

    public override void RemoveNode(Node node)
    {
        if (node is not IStoragePassiveNode)
            _storageSystem.RaiseStorageNetRemoveNodeEvent(node.Owner, new(node, this));
    }
}