using Content.Server.NodeContainer.Nodes;
using Content.Server.StorageSys.NodeGroups;

namespace Content.Server.StorageSys.Nodes;

/// <summary>
/// A storage node that can trigger behavior when loaded and removed by a StorageNet.
/// </summary>
public abstract partial class StorageActiveNode : Node
{
    [ViewVariables]
    public StorageNet? LoadedNet;
}