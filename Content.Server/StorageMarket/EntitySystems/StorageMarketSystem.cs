using Content.Server._NF.CrateMachine;
using Content.Server.Power.EntitySystems;
using Content.Server.Stack;
using Content.Server.StorageSys.EntitySystems;
using Content.Shared.Containers;
using Content.Shared.StorageMarket.EntitySystems;
using Robust.Server.Containers;
using Robust.Server.GameObjects;
using Robust.Shared.Prototypes;

namespace Content.Server.StorageMarket.EntitySystems;

public sealed partial class StorageMarketSystem : SharedStorageMarketSystem
{
    [Dependency] private readonly CrateMachineSystem _crateMachineSystem = default!;
    [Dependency] private readonly TransformSystem _transformSystem = default!;
    [Dependency] private readonly PowerReceiverSystem _powerReceiverSystem = default!;
    [Dependency] private readonly EntityLookupSystem _entityLookupSystem = default!;
    [Dependency] private readonly SharedContainerUtilitySystem _sharedContainerUtilitiesSystem = default!;
    [Dependency] private readonly StorageNetSystem _storageNetSystem = default!;
    [Dependency] private readonly UserInterfaceSystem _userInterfaceSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        InitializeComputers();
    }
}