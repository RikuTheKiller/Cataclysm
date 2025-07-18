using Content.Client.StorageMarket.UI;
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
}