using Content.Shared.Materials;
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

        var price = 0;

        foreach (var materialPair in physicalComposition.MaterialComposition)
        {
            if (!_prototypeManager.TryIndex<MaterialPrototype>(materialPair.Key, out var materialPrototype))
                continue;

            price += materialPrototype.MarketPrice * materialPair.Value;
        }

        return price / 100; // MarketPrice is per 100 units.
    }
}