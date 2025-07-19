using System.Diagnostics.CodeAnalysis;
using Content.Shared.StorageMarket.EntitySystems;
using Content.Shared.StorageMarket.Prototypes;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Shared.Prototypes;

namespace Content.Client.StorageMarket.EntitySystems;

public sealed partial class ClientStorageMarketSystem : SharedStorageMarketSystem
{
    [Dependency] private readonly SpriteSystem _spriteSystem = default!;

    public bool TryGetVisuals(ProtoId<StorageEntryPrototype> protoId, [NotNullWhen(true)] out string? name, [NotNullWhen(true)] out Texture? icon)
    {
        name = null;
        icon = null;

        if (!PrototypeManager.TryIndex(protoId, out var entryPrototype))
            return false;
        if (!PrototypeManager.TryIndex(entryPrototype.EntityPrototype, out var entityPrototype))
            return false;

        name = entityPrototype.Name;

        if (entryPrototype.StackPrototype != null && PrototypeManager.TryIndex(entryPrototype.StackPrototype, out var stackPrototype) && stackPrototype.Icon != null)
            icon = _spriteSystem.Frame0(stackPrototype.Icon);
        else if (entityPrototype.TryGetComponent<SpriteComponent>(out var sprite, ComponentFactory) && sprite.Icon != null)
            icon = sprite.Icon.Default;
        else
            return false;

        return true;
    }
}