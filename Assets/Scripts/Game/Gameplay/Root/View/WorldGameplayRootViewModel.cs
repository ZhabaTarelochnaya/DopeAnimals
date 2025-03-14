using ObservableCollections;

public class WorldGameplayRootViewModel
{
    public readonly IObservableCollection<InteractableViewModel> AllInteractables;
    public WorldGameplayRootViewModel(PlayerInteractionService interactionService)
    {
        AllInteractables = interactionService.AllInteractables;
    }
}