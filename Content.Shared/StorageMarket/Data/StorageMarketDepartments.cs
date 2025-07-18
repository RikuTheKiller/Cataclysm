using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.Data;

[Flags, NetSerializable, Serializable]
public enum StorageMarketDepartments : byte
{
    Industrial = 1 << 0
}