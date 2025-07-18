using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.Data;

[Flags, NetSerializable, Serializable]
public enum StorageMarketCategories : uint
{
    Ore = 1 << 0
}