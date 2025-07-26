using Content.Shared.StorageMarket.Entries;
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

    public int GetSinglePrice(StorageMarketStockUiEntry entry)
    {
        return GetSinglePrice(entry.BasePrice, entry.Quantity, entry.IdealStockCount, entry.MinPriceMultiplier, entry.MaxPriceMultiplier);
    }

    public int GetTotalPrice(int basePrice, int quantity, int stockCount, int idealStockCount, float minPriceMultiplier, float maxPriceMultiplier)
    {
        if (idealStockCount <= 0)
            throw new ArgumentException("'idealStockCount' must be higher than zero.");
        if (quantity <= 0)
            throw new ArgumentException("'quantity' must be higher than zero.");

        var totalPrice = 0;

        for (var i = 0; i < quantity; i++)
        {
            var adjustedStockCount = Math.Max(0, stockCount - i);
            totalPrice += GetSinglePrice(basePrice, adjustedStockCount, idealStockCount, minPriceMultiplier, maxPriceMultiplier);
        }

        return totalPrice;
    }

    public int GetSinglePrice(int basePrice, int stockCount, int idealStockCount, float minPriceMultiplier, float maxPriceMultiplier)
    {
        if (idealStockCount <= 0)
            throw new ArgumentException("'idealStockCount' must be higher than zero.");

        // Calculate stock ratio: 1 = perfect stock, less than 1 = undersupply, more than 1 = oversupply
        var stockRatio = (float)stockCount / idealStockCount;

        // Inverse logic: 2x oversupply = 0.5x price, 0.5x undersupply = 2x price
        // Clamped between 50% and 200% of base price (maybe make the min and max dynamic at some point?)
        var priceMultiplier = Math.Clamp(1f / stockRatio, minPriceMultiplier, maxPriceMultiplier);

        return (int)MathF.Round(basePrice * priceMultiplier);
    }
}