using Robust.Shared.Serialization;

namespace Content.Shared.StorageMarket.Data;

[Flags, FlagsFor(typeof(StorageMarketCategory)), NetSerializable, Serializable]
public enum StorageMarketCategories : int
{
    Ore = 1 << 0,

    All = Ore
}

/// <summary>
/// YAML FlagSerializer type for StorageMarketCategories.
/// </summary>
public sealed class StorageMarketCategory { }

[Flags, FlagsFor(typeof(StorageMarketDepartment)), NetSerializable, Serializable]
public enum StorageMarketDepartments : int
{
    Industrial = 1 << 0,

    All = Industrial
}

/// <summary>
/// YAML FlagSerializer type for StorageMarketDepartments.
/// </summary>
public sealed class StorageMarketDepartment { }

[NetSerializable, Serializable]
public enum StorageMarketMenuTab : byte
{
    Buy,
    Sell,
    Default = Buy
}

[NetSerializable, Serializable]
public sealed class StorageMarketComputerSetTabMessage(StorageMarketMenuTab tab) : BoundUserInterfaceMessage
{
    public readonly StorageMarketMenuTab Tab = tab;
}