using ObservableCollections;
using System;
using UnityEngine;

public class WorldGameplayRootViewModel
{
    public readonly IObservableCollection<InteractableViewModel> AllInteractables;
    public WorldGameplayRootViewModel(PlayerInteractionService interactionService)
    {
        AllInteractables = interactionService.AllInteractables;
    }
}
