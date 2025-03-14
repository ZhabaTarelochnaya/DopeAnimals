using System;
using System.Linq;
using BaCon;

public static class GameplayRegistrations
{
    public static void Register(DIContainer container, GameplayEnterParams enterParams)
    {
        var gameStateProvider = container.Resolve<PlayerPrefsGameStateProvider>();
        Game gameState = gameStateProvider.GameState;
        var settingsProviider = container.Resolve<ISettingsProvider>();

        var cmd = new CommandProcessor();
        cmd.RegisterHandler(new CmdPlaceInteractableHandler(gameState));
        cmd.RegisterHandler(new CmdRemoveInteractableHandler(gameState));
        cmd.RegisterHandler(new CmdInteractHandler(gameState));
        cmd.RegisterHandler(new CmdCreateLevelStateHandler(gameState, container.Resolve<GameSettings>()));
        container.RegisterInstance<ICommandProcessor>(cmd);

        //Fix later. Level state should be loaded before scene load.
        int loadingLevelId = enterParams.LevelId;
        Level loadingLevel = gameState.Levels.FirstOrDefault(l => l.Id == loadingLevelId);
        if (loadingLevel == null)
        {
            var command = new CmdCreateLevelState(loadingLevelId);
            if (!cmd.Process(command))
            {
                throw new Exception($"Couldn't create level state with id: {loadingLevelId}");
            }

            loadingLevel = gameState.Levels.First(l => l.Id == loadingLevelId);
        }

        //container.RegisterFactory(c => 
        //new PlayerInteractionService(gameState.Interactables, cmd)).AsSingle();
    }
}