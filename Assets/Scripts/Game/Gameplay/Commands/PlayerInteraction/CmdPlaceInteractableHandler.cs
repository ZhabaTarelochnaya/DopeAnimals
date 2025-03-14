using System;
using System.Linq;
using UnityEngine;

public class CmdPlaceInteractableHandler : ICommandHandler<CmdPlaceInteractable>
{
    readonly Game _gameState;
    public CmdPlaceInteractableHandler(Game gameState)
    {
        _gameState = gameState;
    }

    public bool Handle(CmdPlaceInteractable command)
    {
        var currentLevel = _gameState.Levels.FirstOrDefault(l => l.Id == _gameState.CurrentLevelId.CurrentValue);
        if (currentLevel == null)
        {
            Debug.Log($"Couldn't find LevelState for id: {_gameState.CurrentLevelId.CurrentValue}");
            return false;
        }
        var entityId = _gameState.CreateEntityId();
        var newInteractableEntity = new InteractableEntityState
        {
            Id = entityId,
            Position = command.Position,
            TypeId = command.InteractableTypeId,
            IsInteractable = command.IsInteractable
        };
        var newInteractableEntityProxy = new InteractableEntity(newInteractableEntity);
        currentLevel.Interactables.Add(newInteractableEntityProxy);
        return true;
    }

}
