using Content.Server.StorageSys.Data;
using Content.Shared.StorageMarket.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageMarket.EntitySystems;

public sealed partial class StorageMarketSystem
{
    public bool IsWhitelisted(ProtoId<StorageEntryPrototype> protoId, StorageMarketWhitelist whitelist)
    {
        if (!PrototypeManager.TryIndex(protoId, out var prototype))
            return false;

        return IsWhitelisted(prototype, whitelist);
    }

    public bool IsWhitelisted(StorageEntryPrototype prototype, StorageMarketWhitelist whitelist)
    {
        // Check categories
        if ((prototype.Categories & whitelist.AllowedCategories) == 0)
            return false;
        if ((prototype.Categories & whitelist.DeniedCategories) != 0)
            return false;

        // Check departments
        if ((prototype.Departments & whitelist.AllowedDepartments) == 0)
            return false;
        if ((prototype.Departments & whitelist.DeniedDepartments) != 0)
            return false;

        // Passed
        return true;
    }
}