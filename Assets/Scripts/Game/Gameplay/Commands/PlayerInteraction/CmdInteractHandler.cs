using R3;
using UnityEngine;

public class CmdInteractHandler : ICommandHandler<CmdInteract>
{
    readonly Game _gameState;
    public CmdInteractHandler(Game gameState)
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
                // Replace later with dependant on BuildingTypeId logic
                Debug.Log($"Interaction with entity {command.Id}");
                return true;
            }
        }

        Debug.Log($"Entity with id {command.Id} does not exist");
        return false;
    }
}