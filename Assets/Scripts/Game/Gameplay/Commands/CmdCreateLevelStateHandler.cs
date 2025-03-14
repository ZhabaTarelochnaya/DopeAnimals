using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CmdCreateLevelStateHandler : ICommandHandler<CmdCreateLevelState>
{
    private readonly Game _game;
    private readonly GameSettings _gameSettings;

    public CmdCreateLevelStateHandler(Game gameState, GameSettings gameSettings)
    {
        _game = gameState;
        _gameSettings = gameSettings;
    }

    public bool Handle(CmdCreateLevelState command)
    {
        var isMapAlreadyExisted = _game.Levels.Any(m => m.Id == command.LevelId);

        if (isMapAlreadyExisted)
        {
            Debug.LogError($"Level with Id = {command.LevelId} already exists");
            return false;
        }

        var newLevelSettings = _gameSettings.LevelsSettings.Levels.First(l => l.Id == command.LevelId);
        var newLevelInitialStateSettings = newLevelSettings.InitialStateSettings;

        var initialInteractables = new List<IInteractableEntityState>();
        foreach (var interactableSettings in newLevelInitialStateSettings.Interactables)
        {
            var initialInteractable = new InteractableEntityState
            {
                Id = _game.CreateEntityId(),
                TypeId = interactableSettings.TypeId,
                Position = interactableSettings.Position,
                IsInteractable = interactableSettings.IsInteractable
            };

            initialInteractables.Add(initialInteractable);
        }

        var newLevelState = new LevelState
        {
            Id = command.LevelId,
            Interactables = initialInteractables
        };

        var newLevel = new Level(newLevelState);

        _game.Levels.Add(newLevel);

        return true;
    }
}
