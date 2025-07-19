using System.Diagnostics.CodeAnalysis;
using Content.Server.StorageMarket.Components;
using Content.Server.StorageSys.Components;
using Content.Server.StorageSys.NodeGroups;
using Content.Shared._NF.CrateMachine.Components;

namespace Content.Server.StorageMarket.EntitySystems;

public sealed partial class StorageMarketSystem
{
    public IEnumerable<Entity<StorageMarketSellPalletComponent, TransformComponent>> GetConnectedPallets(Entity<StorageMarketComputerComponent?, TransformComponent?> computer)
    {
        if (!Resolve(computer, ref computer.Comp1, ref computer.Comp2))
            yield break;
        if (!computer.Comp2.Anchored || computer.Comp2.GridUid == null)
            yield break;
        if (!_powerReceiverSystem.IsPowered(computer))
            yield break;

        var query = EntityQueryEnumerator<StorageMarketSellPalletComponent, TransformComponent>();

        while (query.MoveNext(out var palletUid, out var pallet, out var palletTransform))
        {
            if (!palletTransform.Anchored || computer.Comp2.GridUid != palletTransform.GridUid)
                continue;
            if (!_transformSystem.InRange((palletUid, palletTransform), (computer, computer.Comp2), computer.Comp1.MachineRange))
                continue;

            yield return (palletUid, pallet, palletTransform);
        }
    }

    public IEnumerable<EntityUid> GetSellables(Entity<StorageMarketComputerComponent?, TransformComponent?> computer)
    {
        foreach (var pallet in GetConnectedPallets(computer))
            foreach (var sellableUid in GetSellables(pallet.AsNullable()))
                yield return sellableUid;
    }

    public IEnumerable<EntityUid> GetSellables(Entity<StorageMarketSellPalletComponent?, TransformComponent?> pallet)
    {
        if (!Resolve(pallet, ref pallet.Comp1, ref pallet.Comp2))
            yield break;
        if (!pallet.Comp2.Anchored || pallet.Comp2.GridUid == null)
            yield break;

        foreach (var rootSellableUid in _entityLookupSystem.GetEntitiesIntersecting(pallet, LookupFlags.Dynamic | LookupFlags.Sundries))
        {
            foreach (var sellableUid in _sharedContainerUtilitiesSystem.GetContentsAndSelf(rootSellableUid))
            {
                if (!TryComp(sellableUid, out TransformComponent? sellableTransform))
                    continue;
                if (sellableTransform.Anchored)
                    continue;

                yield return sellableUid;
            }
        }
    }

    public IEnumerable<Entity<CrateMachineComponent, TransformComponent>> GetConnectedCrateMachines(Entity<StorageMarketComputerComponent?, TransformComponent?> computer)
    {
        if (!Resolve(computer, ref computer.Comp1, ref computer.Comp2))
            yield break;
        if (!computer.Comp2.Anchored || computer.Comp2.GridUid == null)
            yield break;
        if (!_powerReceiverSystem.IsPowered(computer))
            yield break;

        var query = EntityQueryEnumerator<CrateMachineComponent, TransformComponent>();

        while (query.MoveNext(out var crateMachineUid, out var crateMachine, out var crateMachineTransform))
        {
            if (!crateMachineTransform.Anchored || computer.Comp2.GridUid != crateMachineTransform.GridUid)
                continue;
            if (!_transformSystem.InRange((crateMachineUid, crateMachineTransform), (computer, computer.Comp2), computer.Comp1.MachineRange))
                continue;

            yield return (crateMachineUid, crateMachine, crateMachineTransform);
        }
    }

    public bool TryGetStorageNet(EntityUid entity, [NotNullWhen(true)] out StorageNet? net)
    {
        return _storageNetSystem.TryGetStorageNet(entity, out net);
    }

    public bool Resolve(EntityUid entity, [NotNullWhen(true)] ref StorageNet? net)
    {
        return net != null || TryGetStorageNet(entity, out net);
    }
}