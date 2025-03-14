using System;
using UnityEngine;

public class CmdRemoveInteractableHandler : ICommandHandler<CmdInteract>
{
    readonly Game _gameState;
    public CmdRemoveInteractableHandler(Game gameState)
    {
        _gameState = gameState;
    }

    public bool Handle(CmdInteract command)
    {
        foreach (IInteractableEntity interactable in _gameState.Interactables)
        {
            if (interactable.Id == command.Id && interactable.IsInteractable.Value)
            {
                _gameState.Interactables.Remove(interactable);
                return true;
            }
        }
        Debug.Log($"Entity with id {command.Id} does not exist");
        return false;
    }
}
