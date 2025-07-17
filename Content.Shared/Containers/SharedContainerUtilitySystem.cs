using Robust.Shared.Containers;

namespace Content.Shared.Containers;

public sealed partial class SharedContainerUtilitySystem : EntitySystem
{
    /// <summary>
    /// Enumerates over all contents within the entity.
    /// </summary>
    public IEnumerable<EntityUid> GetContents(EntityUid rootEntity)
    {
        if (!TryComp<ContainerManagerComponent>(rootEntity, out var containerManager))
            yield break;

        Stack<EntityUid> stack = new();

        foreach (var container in containerManager.Containers.Values)
            foreach (var containedEntity in container.ContainedEntities)
                stack.Push(containedEntity);

        while (stack.TryPop(out var entity))
        {
            yield return entity;

            if (!TryComp(entity, out containerManager))
                continue;

            foreach (var container in containerManager.Containers.Values)
                foreach (var containedEntity in container.ContainedEntities)
                    stack.Push(containedEntity);
        }
    }

    /// <summary>
    /// Enumerates over the entity and all contents within the entity.
    /// </summary>
    public IEnumerable<EntityUid> GetContentsAndSelf(EntityUid rootEntity)
    {
        Stack<EntityUid> stack = new();

        stack.Push(rootEntity);

        while (stack.TryPop(out var entity))
        {
            yield return entity;

            if (!TryComp<ContainerManagerComponent>(entity, out var containerManager))
                continue;

            foreach (var container in containerManager.Containers.Values)
                foreach (var containedEntity in container.ContainedEntities)
                    stack.Push(containedEntity);
        }
    }
}