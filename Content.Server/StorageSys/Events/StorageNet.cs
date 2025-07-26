using Content.Server.StorageSys.NodeGroups;
using Content.Server.StorageSys.Nodes;

namespace Content.Server.StorageSys.Events;

public sealed class StorageNetLoadNodeEvent : EventArgs
{
    public StorageActiveNode Node { get; }
    public StorageNet Net { get; }

    public StorageNetLoadNodeEvent(StorageActiveNode node, StorageNet net)
    {
        Node = node;
        Net = net;
    }
}

public sealed class StorageNetRemoveNodeEvent : EventArgs
{
    public StorageActiveNode Node { get; }
    public StorageNet Net { get; }

    public StorageNetRemoveNodeEvent(StorageActiveNode node, StorageNet net)
    {
        Node = node;
        Net = net;
    }
}