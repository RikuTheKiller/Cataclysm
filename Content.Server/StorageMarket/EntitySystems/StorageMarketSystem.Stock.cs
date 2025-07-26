using Content.Server.StorageSys.Components;
using Content.Shared.StorageMarket.Prototypes;

namespace Content.Server.StorageMarket.EntitySystems;

public sealed partial class StorageMarketSystem
{
    public void PopulateStock(EntityUid uid, StorageControllerDriveComponent comp)
    {
        if (comp.InitialMarketStockWhitelist == null)
            return;

        foreach (var prototype in PrototypeManager.EnumeratePrototypes<StorageEntryPrototype>())
            if (IsWhitelisted(prototype, comp.InitialMarketStockWhitelist))
                comp.Data?.MarketData.Stock.Add(prototype.ID, new(prototype, 30, 0.5f, 2f)); // Demagificy later
    }
}