using R3;
using System;
using UnityEngine;

public class InteractableViewModel
{
    readonly IInteractableEntity _interactableEntity;
    readonly PlayerInteractionService _playerInteractionService;

    public readonly int Id;
    public ReadOnlyReactiveProperty<Vector3> Position { get; }

    public InteractableViewModel(IInteractableEntity interactableEntity, PlayerInteractionService interactionService)
    {
        _interactableEntity = interactableEntity;
        _playerInteractionService = interactionService;

        Id = interactableEntity.Id;
        Position = interactableEntity.Position;
    }
}
 