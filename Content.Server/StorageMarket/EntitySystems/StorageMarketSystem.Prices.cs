using Content.Shared.Materials;
using Content.Shared.Stacks;
using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageMarket.EntitySystems;

public sealed partial class StorageMarketSystem : EntitySystem
{
    public int GetPrice(StorageEntryPrototype prototype)
    {
        if (prototype.Prototype != null)
            return GetPrice(prototype.Prototype.Value);
        if (prototype.StackPrototype != null)
            return GetPrice(prototype.StackPrototype.Value);

        return 0;
    }

    public int GetPrice(EntProtoId prototypeId)
    {
        if (!_prototypeManager.TryIndex(prototypeId, out var prototype))
            return 0;
        if (!prototype.TryGetComponent<PhysicalCompositionComponent>(out var physicalComposition, _componentFactory))
            return 0;

        return GetPrice(physicalComposition);
    }

    public int GetPrice(ProtoId<StackPrototype> prototypeId)
    {
        if (!_prototypeManager.TryIndex(prototypeId, out var stackPrototype))
            return 0;
        if (!_prototypeManager.TryIndex(stackPrototype.Spawn, out var entityPrototype))
            return 0;
        if (!entityPrototype.TryGetComponent<PhysicalCompositionComponent>(out var physicalComposition, _componentFactory))
            return 0;

        return GetPrice(physicalComposition);
    }

    public int GetPrice(EntityUid uid)
    {
        var total = 0;

        foreach (var entityUid in _sharedContainerUtilitiesSystem.GetContentsAndSelf(uid))
            if (TryComp<PhysicalCompositionComponent>(entityUid, out var physicalComposition))
                total += GetPrice(entityUid, physicalComposition);

        return total;
    }

    public int GetPrice(EntityUid uid, PhysicalCompositionComponent physicalComposition)
    {
        var price = GetPrice(physicalComposition);

        if (TryComp<StackComponent>(uid, out var stack))
            price *= stack.Count;

        return price;
    }

    public int GetPrice(PhysicalCompositionComponent physicalComposition)
    {
        var price = 0.0;

        foreach (var materialPair in physicalComposition.MaterialComposition)
        {
            if (!_prototypeManager.TryIndex<MaterialPrototype>(materialPair.Key, out var materialPrototype))
                continue;

            price += materialPrototype.Price * materialPair.Value;
        }

        return (int)price;
    }
}