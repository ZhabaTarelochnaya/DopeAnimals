using System;
using UnityEngine;

public class CmdPlaceInteractableHandler : ICommandHandler<CmdPlaceInteractable>
{
    readonly GameStateProxy _gameState;
    public CmdPlaceInteractableHandler(GameStateProxy gameState)
    {
        _gameState = gameState;
    }

    public bool Handle(CmdPlaceInteractable command)
    {
        var entityId = _gameState.GetEntityId();
        var newInteractableEntity = new InteractableEntity
        {
            Id = entityId,
            Position = command.Position,
            InteractableTypeID = command.InteractableTypeId,
            IsInteractable = command.IsInteractable
        };
        var newInteractableEntityProxy = new InteractableEntityProxy(newInteractableEntity);
        _gameState.Interactables.Add(newInteractableEntityProxy);
        return true;
    }

}
