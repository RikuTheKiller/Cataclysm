using Content.Shared.Materials;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageSys.Components;

[RegisterComponent]
public sealed partial class StorageContainerComponent : Component
{
    /// <summary>
    /// Per-item maximum storage capacity. A tiny item consumes 1 capacity, small 2, normal 4, so on and so forth.
    /// </summary>
    [DataField(required: true)]
    public int Capacity;

    public Dictionary<ProtoId<MaterialPrototype>, int> Stored;
}