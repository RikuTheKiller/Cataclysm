using Content.Server.StorageSys.EntitySystems;
using Content.Server.StorageSys.Nodes;
using Content.Server.NodeContainer.NodeGroups;
using Content.Server.NodeContainer.Nodes;
using Content.Server.StorageSys.Data;
using System.Linq;

namespace Content.Server.StorageSys.NodeGroups;

[NodeGroup(NodeGroupID.Storage)]
public sealed partial class StorageNet : BaseNodeGroup
{
    private StorageNetSystem _storageSystem = default!;

    [ViewVariables]
    public readonly List<EntityUid> Controllers = [];

    [ViewVariables]
    public readonly List<EntityUid> MaterialContainers = [];

    [ViewVariables]
    public readonly List<EntityUid> ItemContainers = [];

    [ViewVariables]
    public StorageControllerData? ControllerData;

    public override void Initialize(Node sourceNode, IEntityManager entMan)
    {
        base.Initialize(sourceNode, entMan);

        _storageSystem = entMan.System<StorageNetSystem>();
    }

    public override void LoadNodes(List<Node> groupNodes)
    {
        base.LoadNodes(groupNodes);

        foreach (var node in groupNodes)
        {
            if (node is not StorageActiveNode storageNode)
                continue;
            if (storageNode.LoadedNet != null)
                _storageSystem.StorageNetRemoveNode(node.Owner, new(storageNode, storageNode.LoadedNet));

            _storageSystem.StorageNetLoadNode(node.Owner, new(storageNode, this));
        }
    }

    public override void RemoveNode(Node node)
    {
        if (node is not StorageActiveNode storageNode)
            return;
        if (storageNode.LoadedNet != this)
            return;

        _storageSystem.StorageNetRemoveNode(node.Owner, new(storageNode, this));
    }
}