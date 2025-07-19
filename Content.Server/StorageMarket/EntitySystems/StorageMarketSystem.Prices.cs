using Content.Shared.Materials;
using Content.Shared.Stacks;
using Content.Shared.StorageMarket.EntitySystems;
using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageMarket.EntitySystems;

public sealed partial class StorageMarketSystem
{
    public int GetBasePrice(ProtoId<StorageEntryPrototype> protoId)
    {
        if (!PrototypeManager.TryIndex(protoId, out var prototype))
            return 0;

        return GetBasePrice(prototype);
    }

    public int GetBasePrice(StorageEntryPrototype prototype)
    {
        return GetBasePrice(prototype.EntityPrototype);
    }

    public int GetBasePrice(EntProtoId prototypeId)
    {
        if (!PrototypeManager.TryIndex(prototypeId, out var prototype))
            return 0;
        if (!prototype.TryGetComponent<PhysicalCompositionComponent>(out var physicalComposition, ComponentFactory))
            return 0;

        return GetBasePrice(physicalComposition);
    }

    public int GetBasePrice(EntityUid uid)
    {
        if (!TryComp<PhysicalCompositionComponent>(uid, out var physicalComposition))
            return 0;

        var price = GetBasePrice(physicalComposition);

        if (TryComp<StackComponent>(uid, out var stack))
            price *= stack.Count;

        return price;
    }

    public int GetBasePrice(PhysicalCompositionComponent physicalComposition)
    {
        var price = 0.0;

        foreach (var materialPair in physicalComposition.MaterialComposition)
        {
            if (!PrototypeManager.TryIndex<MaterialPrototype>(materialPair.Key, out var materialPrototype))
                continue;

            price += materialPrototype.Price * materialPair.Value;
        }

        return (int)Math.Round(price);
    }
}