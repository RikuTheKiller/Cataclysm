using Content.Client.StorageMarket.UI;
using Content.Shared.StorageMarket.BUI;
using Robust.Client.UserInterface;

namespace Content.Client.StorageMarket.BUI;

public sealed class StorageMarketComputerBoundUserInterface(EntityUid owner, Enum uiKey) : BoundUserInterface(owner, uiKey)
{
    private StorageMarketMenu? _menu;

    protected override void Open()
    {
        base.Open();

        _menu = this.CreateWindow<StorageMarketMenu>();
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        if (state is StorageMarketComputerInterfaceState verifiedState)
            _menu?.UpdateState(verifiedState);
    }
}