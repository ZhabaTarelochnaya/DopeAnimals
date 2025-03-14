using R3;
using UnityEngine;

public class InteractableEntity : IInteractableEntity
{
    public InteractableEntity(IInteractableEntityState interactableState)
    {
        Origin = interactableState;
        Id = interactableState.Id;
        IsInteractable = new ReactiveProperty<bool>(interactableState.IsInteractable);
        Position = new ReactiveProperty<Vector3>(interactableState.Position);

        IsInteractable.Skip(1).Subscribe(e => interactableState.IsInteractable = e);
        Position.Skip(1).Subscribe(e => interactableState.Position = e);
    }
    public string InteractableTypeId { get; }
    public int Id { get; }
    public IInteractableEntityState Origin { get; }
    public ReactiveProperty<bool> IsInteractable { get; }

    public ReactiveProperty<Vector3> Position { get; }
}