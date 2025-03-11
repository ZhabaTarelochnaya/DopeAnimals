using System;
using UnityEngine;

public class CmdInteractHandler : ICommandHandler<CmdInteract>
{
    readonly GameStateProxy _gameState;
    public CmdInteractHandler(GameStateProxy gameState)
    {
        _gameState = gameState;
    }

    public bool Handle(CmdInteract command)
    {
        var entityId = _gameState.GetEntityId();
        var interactableEntity = new InteractableEntity
        {
            Id = entityId,
            Position = command.Position,
            IsInteractable = command.IsInteractable,
        };
        var newInteractableEntityProxy = new InteractableEntityProxy(interactableEntity);
        _gameState.Interactables.Add(newInteractableEntityProxy);
        return true;
    }

}
