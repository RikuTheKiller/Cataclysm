using Content.Shared.StorageMarket.Prototypes;
using Content.Server.StorageSys.Components;
using Content.Shared.StorageMarket.BUI;
using Content.Shared.StorageMarket.Data;
using Robust.Shared.Prototypes;
using System.Diagnostics.CodeAnalysis;
using Content.Server.StorageSys.NodeGroups;

namespace Content.Server.StorageMarket.EntitySystems;

public sealed partial class StorageMarketSystem : EntitySystem
{
    public void InitializeComputers()
    {
        SubscribeLocalEvent<StorageMarketComputerComponent, BoundUIOpenedEvent>(OnComputerUIOpened);
    }

    private void OnComputerUIOpened(EntityUid uid, StorageMarketComputerComponent comp, BoundUIOpenedEvent args)
    {
        RefreshEntries(uid, comp);
        RefreshState(uid, comp);
    }

    public void RefreshState(EntityUid uid, StorageMarketComputerComponent? computer = null)
    {
        if (!Resolve(uid, ref computer))
            return;

        StorageMarketComputerInterfaceState state = new(computer.Entries, computer.BuyCart, computer.SellCart);

        _userInterfaceSystem.SetUiState(uid, StorageMarketComputerUiKey.Default, state);
    }

    public void RefreshEntries(EntityUid uid, StorageMarketComputerComponent? computer = null)
    {
        if (!Resolve(uid, ref computer))
            return;

        if (TryGetEntries((uid, computer), out var entries))
            computer.Entries = entries;
        else
            computer.Entries.Clear();
    }

    public bool TryGetEntries(Entity<StorageMarketComputerComponent?> entity, [NotNullWhen(true)] out List<StorageMarketEntry>? entries)
    {
        entries = null;

        if (!Resolve(entity, ref entity.Comp))
            return false;
        if (!_storageNetSystem.TryGetStorageNet(entity, out var net))
            return false;
        if (net.ControllerData == null)
            return false;

        entries = new();

        foreach (var protoId in net.ControllerData.MarketData.Entries)
            if (TryCreateEntry(protoId, net, out var entry))
                entries.Add(entry.Value);

        entries.Sort(); // This sorts the entries in OrdinalIgnoreCase (basically case-insensitive alphabetical) order.

        return true;
    }

    public bool TryCreateEntry(ProtoId<StorageEntryPrototype> protoId, StorageNet net, [NotNullWhen(true)] out StorageMarketEntry? entry)
    {
        entry = null;

        if (!_prototypeManager.TryIndex(protoId, out var entryPrototype))
            return false;
        if (entryPrototype.Prototype == null && entryPrototype.StackPrototype == null)
            return false;

        entry = new(entryPrototype, GetBasePrice(entryPrototype), _storageNetSystem.GetEntryCount(protoId, net), false);
        return true;
    }
}