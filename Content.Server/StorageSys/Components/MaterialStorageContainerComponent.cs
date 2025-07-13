using Content.Shared.Materials;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageSys.Components;

[RegisterComponent]
public sealed partial class MaterialStorageContainerComponent : Component
{
    public Dictionary<ProtoId<MaterialPrototype>, int> Storage = [];

    /// <summary>
    /// Per-type capacity for materials. 1 sheet uses roughly 100 capacity.
    /// </summary>
    [DataField(required: true)]
    public int Capacity;
}