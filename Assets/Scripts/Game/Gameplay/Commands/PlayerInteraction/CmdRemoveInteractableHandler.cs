using R3;
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
        ReactiveProperty<int> currentLevelId = _gameState.CurrentLevelId;
        Level currentLevel = _gameState.Levels[currentLevelId.Value];
        foreach (IInteractableEntity interactable in currentLevel.Interactables)
        {
            if (interactable.Id == command.Id && interactable.IsInteractable.Value)
            {
                currentLevel.Interactables.Remove(interactable);
                return true;
            }
        }

        Debug.Log($"Entity with id {command.Id} does not exist");
        return false;
    }
}