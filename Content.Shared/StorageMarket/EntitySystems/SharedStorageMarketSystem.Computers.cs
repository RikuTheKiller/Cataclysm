using Content.Shared.StorageMarket.BUI;
using Content.Shared.StorageMarket.Data;
using Content.Shared.StorageMarket.Components;

namespace Content.Shared.StorageMarket.EntitySystems;

public partial class SharedStorageMarketSystem
{
    [MustCallBase(true)]
    public virtual void InitializeComputers()
    {
        SubscribeLocalEvent<StorageMarketComputerComponent, StorageMarketComputerSetTabMessage>(OnStorageMarketComputerSetTabMessage);
    }

    private void OnStorageMarketComputerSetTabMessage(EntityUid uid, StorageMarketComputerComponent comp, StorageMarketComputerSetTabMessage args)
    {
        var state = GetMarketComputerUiState(uid);

        state.Tab = args.Tab;

        SetMarketComputerUiState(uid, state);
    }

    /// <summary>
    /// Either gets a copy of the existing UI state or a blank UI state.
    /// </summary>
    public StorageMarketComputerInterfaceState GetMarketComputerUiState(EntityUid uid)
    {
        if (SharedUserInterfaceSystem.TryGetUiState<StorageMarketComputerInterfaceState>(uid, StorageMarketComputerUiKey.Default, out var state))
            return new(state);
        return new();
    }

    /// <summary>
    /// Sets the UI state.
    /// </summary>
    public void SetMarketComputerUiState(EntityUid uid, StorageMarketComputerInterfaceState? state)
    {
        SharedUserInterfaceSystem.SetUiState(uid, StorageMarketComputerUiKey.Default, state);
    }
}