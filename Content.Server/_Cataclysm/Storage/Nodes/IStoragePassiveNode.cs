namespace Content.Server._Cataclysm.Storage.Nodes;

/// <summary>
/// Makes StorageNet not raise an event when this node type is loaded or removed.
/// Avoids the performance cost of raising an event for every single storage duct.
/// </summary>
public interface IStoragePassiveNode { }