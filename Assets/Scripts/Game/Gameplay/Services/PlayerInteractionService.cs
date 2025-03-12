using ObservableCollections;
using R3;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionService
{
    readonly ICommandProcessor _cmd;
    readonly ObservableList<InteractableViewModel> _allInteractables = new();
    readonly Dictionary<int, InteractableViewModel> _interactableMap = new();
    public IObservableCollection<InteractableViewModel> AllInteractables => _allInteractables;

    public PlayerInteractionService(ObservableList<IInteractableEntityProxy> interactables, ICommandProcessor cmd)
    {
        _cmd = cmd;
        foreach (var interactable in interactables)
        {
            CreateInteractableViewModel(interactable);
        }
        interactables.ObserveAdd().Subscribe(e =>
        {
            CreateInteractableViewModel(e.Value);
        });
        interactables.ObserveRemove().Subscribe(e =>
        {
            RemoveInteractableViewModel(e.Value);
        });
    }
    void CreateInteractableViewModel(IInteractableEntityProxy interactableEntity)
    {
        var interacctableViewModel = new InteractableViewModel(interactableEntity);
        _interactableMap[interactableEntity.Id] = interacctableViewModel;
        _allInteractables.Add(interacctableViewModel);
    }
    void RemoveInteractableViewModel(IInteractableEntityProxy interactableEntity)
    {
        if (_interactableMap.TryGetValue(interactableEntity.Id, out var interactableViewModel))
        {
            _interactableMap.Remove(interactableEntity.Id);
            _allInteractables.Remove(interactableViewModel);
        }
    }
    public bool Interact(int Id)
    {
        return _cmd.Process(new CmdInteract(Id));
    }
    public bool PlaceInteractable(string interactableTypeId, Vector3 position, bool isInteractable = true)
    {
        return _cmd.Process(new CmdPlaceInteractable(interactableTypeId, position, isInteractable));
    }
}
