using Content.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.StorageMarket.UI;

public sealed partial class StorageMarketMenu : FancyWindow
{
    public StorageMarketMenu()
    {
        RobustXamlLoader.Load(this);
    }
}