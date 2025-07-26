using Content.Client.StorageMarket.UI;
using Content.Shared.StorageMarket.BUI;
using Content.Shared.StorageMarket.Data;
using Robust.Client.UserInterface;

namespace Content.Client.StorageMarket.BUI;

public sealed class StorageMarketComputerBoundUserInterface(EntityUid owner, Enum uiKey) : BoundUserInterface(owner, uiKey)
{
    private StorageMarketMenu? _menu;

    protected override void Open()
    {
        base.Open();

        _menu = this.CreateWindow<StorageMarketMenu>();
        _menu.Owner = Owner;
        _menu.OnSetTab += tab => SendPredictedMessage(new StorageMarketComputerSetTabMessage(tab));
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        if (state is StorageMarketComputerInterfaceState verifiedState)
            _menu?.UpdateState(verifiedState);
    }
}