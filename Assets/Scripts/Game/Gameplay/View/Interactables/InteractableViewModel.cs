using R3;
using UnityEngine;

public class InteractableViewModel
{
    readonly IInteractableEntity _interactableEntity;
    readonly PlayerInteractionService _playerInteractionService;

    public readonly int Id;

    public InteractableViewModel(IInteractableEntity interactableEntity, PlayerInteractionService interactionService)
    {
        _interactableEntity = interactableEntity;
        _playerInteractionService = interactionService;

        Id = interactableEntity.Id;
        Position = interactableEntity.Position;
    }
    public ReadOnlyReactiveProperty<Vector3> Position { get; }
}