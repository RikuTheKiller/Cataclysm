using Content.Shared.StorageMarket.Data;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Server.StorageSys.Data;

[DataDefinition]
public sealed partial class StorageMarketWhitelist
{
    [DataField(customTypeSerializer: typeof(FlagSerializer<StorageMarketCategory>))]
    public int AllowedCategories;

    [DataField(customTypeSerializer: typeof(FlagSerializer<StorageMarketCategory>))]
    public int DeniedCategories;

    [DataField(customTypeSerializer: typeof(FlagSerializer<StorageMarketDepartment>))]
    public int AllowedDepartments;

    [DataField(customTypeSerializer: typeof(FlagSerializer<StorageMarketDepartment>))]
    public int DeniedDepartments;
}