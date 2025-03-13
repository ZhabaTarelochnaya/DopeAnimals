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
        foreach (IInteractableEntityProxy interactable in _gameState.Interactables)
        {
            if (interactable.Id == command.Id && interactable.IsInteractable.Value)
            {
                // Replace later with dependant on BuildingTypeId logic
                Debug.Log($"Interaction with entity {command.Id}");
                return true;
            }
        }
        Debug.Log($"Entity with id {command.Id} does not exist");
        return false;
    }

}
