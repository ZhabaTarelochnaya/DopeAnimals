using BaCon;
using System;
using UnityEngine;

public static class GameplayViewModelsRegistrations
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(c => new UIGameplayRootViewModel()).AsSingle();
        container.RegisterFactory(c => new WorldGameplayRootViewModel(c.Resolve<PlayerInteractionService>())).AsSingle();
    }
}
