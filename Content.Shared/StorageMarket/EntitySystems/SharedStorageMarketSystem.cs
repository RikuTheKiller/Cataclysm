using Robust.Shared.Prototypes;

namespace Content.Shared.StorageMarket.EntitySystems;

public abstract partial class SharedStorageMarketSystem : EntitySystem
{
    [Dependency] protected readonly IPrototypeManager PrototypeManager = default!;
    [Dependency] protected readonly IComponentFactory ComponentFactory = default!;
    [Dependency] protected readonly SharedUserInterfaceSystem SharedUserInterfaceSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        InitializeComputers();
    }
}