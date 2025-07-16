using Content.Shared.Materials;
using Content.Shared.Stacks;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageMarket.EntitySystems;

public sealed partial class StorageMarketSystem : EntitySystem
{
    public int GetPrice(EntProtoId prototypeId)
    {
        if (!_prototypeManager.TryIndex<EntityPrototype>(prototypeId, out var prototype))
            return 0;
        if (!prototype.TryGetComponent<PhysicalCompositionComponent>(out var physicalComposition, _componentFactory))
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