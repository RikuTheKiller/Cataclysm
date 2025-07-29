using Content.Shared.StorageMarket.BUI;
using Content.Shared.StorageMarket.Entries;
using Robust.Shared.Prototypes;
using System.Diagnostics.CodeAnalysis;
using Content.Server.StorageSys.NodeGroups;
using Content.Shared.Stacks;
using Content.Shared.StorageMarket.Components;
using Content.Shared.StorageMarket.Prototypes;

namespace Content.Server.StorageMarket.EntitySystems;

public sealed partial class StorageMarketSystem
{
    public override void InitializeComputers()
    {
        base.InitializeComputers();

        SubscribeLocalEvent<StorageMarketComputerComponent, BoundUIOpenedEvent>(OnComputerUIOpened);
    }

    private void OnComputerUIOpened(EntityUid uid, StorageMarketComputerComponent comp, BoundUIOpenedEvent args)
    {
        RefreshState((uid, comp));
    }

    private void RefreshState(Entity<StorageMarketComputerComponent?> computer)
    {
        var state = GetMarketComputerUiState(computer);

        state.Stock = GetStock(computer);
        state.SellCart = GetSellCart(computer);
        state.BuyCart = GetBuyCart(state); // Must be called after updating 'state.Stock'

        SetMarketComputerUiState(computer, state);
    }

    private Dictionary<EntProtoId, StorageMarketStockUiEntry> GetStock(Entity<StorageMarketComputerComponent?> computer)
    {
        if (!TryGetStorageNet(computer, out var net))
            return new();
        if (net.ControllerData == null)
            return new();

        Dictionary<EntProtoId, StorageMarketStockUiEntry> entries = new();

        foreach (var entry in net.ControllerData.MarketData.Stock.Values)
            if (PrototypeManager.TryIndex(entry.Prototype, out var entryPrototype))
                entries.Add(entryPrototype.EntityPrototype, GetStockUiEntry(entry, net));

        return entries;
    }

    private StorageMarketStockUiEntry GetStockUiEntry(StorageMarketItemStockEntry entry, StorageNet net)
    {
        return new(
            entry: entry,
            basePrice: GetBasePrice(entry.Prototype),
            quantity: _storageNetSystem.GetEntryCount(entry.Prototype, net),
            isCraftable: true // Demagificy later
        );
    }

    private List<StorageMarketSellCartUiEntry> GetSellCart(Entity<StorageMarketComputerComponent?> computer)
    {
        List<StorageMarketSellCartUiEntry> entries = new();

        foreach (var sellable in GetSellables(computer))
            if (TryGetSellCartEntry(sellable, out var entry))
                entries.Add(entry);

        return entries;
    }

    private bool TryGetSellCartEntry(Entity<StorageMarketComputerComponent?> computer, [NotNullWhen(true)] out StorageMarketSellCartUiEntry? entry)
    {
        entry = null;

        if (!TryComp(computer, out MetaDataComponent? metaData))
            return false;
        if (metaData.EntityPrototype == null)
            return false;

        entry = new(
            prototype: metaData.EntityPrototype,
            basePrice: GetBasePrice(metaData.EntityPrototype),
            quantity: TryComp<StackComponent>(computer, out var stack) ? stack.Count : 1
        );

        return true;
    }

    private List<StorageMarketBuyCartUiEntry> GetBuyCart(StorageMarketComputerInterfaceState state)
    {
        List<StorageMarketBuyCartUiEntry> validEntries = new();

        foreach (var entry in state.BuyCart)
            if (TryValidateBuyCartEntry(entry, state))
                validEntries.Add(entry);

        return validEntries;
    }

    private bool TryValidateBuyCartEntry(StorageMarketBuyCartUiEntry entry, StorageMarketComputerInterfaceState state)
    {
        if (!PrototypeManager.TryIndex(entry.Prototype, out var entryPrototype))
            return false;
        if (!state.Stock.TryGetValue(entryPrototype.EntityPrototype, out var uiEntry))
            return false;

        entry.Quantity = Math.Max(entry.Quantity, uiEntry.StockCount);
        return true;
    }
}