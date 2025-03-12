using BaCon;
using System;
using UnityEngine;

public static class GameplayRegistarations
{
    public static void Register(DIContainer container, GameplayEnterParams enterParams)
    {
        var gameStateProvider = container.Resolve<PlayerPrefsGameStateProvider>();
        var gameState = gameStateProvider.GameState;

        var cmd = new CommandProcessor();
        cmd.RegisterHandler(new CmdPlaceInteractableHandler(gameState));
        cmd.RegisterHandler(new CmdRemoveInteractableHandler(gameState));
        cmd.RegisterHandler(new CmdInteractHandler(gameState));
        container.RegisterInstance<ICommandProcessor>(cmd);

        container.RegisterFactory(c => 
        new PlayerInteractionService(gameState.Interactables, cmd)).AsSingle();
    }
}
