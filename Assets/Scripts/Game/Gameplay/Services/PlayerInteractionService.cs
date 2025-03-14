using System.Collections.Generic;
using ObservableCollections;
using R3;
using UnityEngine;

public class PlayerInteractionService
{
    readonly ObservableList<InteractableViewModel> _allInteractables = new();
    readonly ICommandProcessor _cmd;
    readonly Dictionary<int, InteractableViewModel> _interactableMap = new();

    public PlayerInteractionService(ObservableList<IInteractableEntity> interactables,
        ICommandProcessor cmd)
    {
        _cmd = cmd;
        foreach (IInteractableEntity interactable in interactables)
        {
            CreateInteractableViewModel(interactable);
        }

        interactables.ObserveAdd().Subscribe(e => { CreateInteractableViewModel(e.Value); });
        interactables.ObserveRemove().Subscribe(e => { RemoveInteractableViewModel(e.Value); });
    }
    public IObservableCollection<InteractableViewModel> AllInteractables => _allInteractables;
    void CreateInteractableViewModel(IInteractableEntity interactableEntity)
    {
        var interacctableViewModel = new InteractableViewModel(interactableEntity, this);
        _interactableMap[interactableEntity.Id] = interacctableViewModel;
        _allInteractables.Add(interacctableViewModel);
    }
    void RemoveInteractableViewModel(IInteractableEntity interactableEntity)
    {
        if (_interactableMap.TryGetValue(interactableEntity.Id, out InteractableViewModel interactableViewModel))
        {
            _interactableMap.Remove(interactableEntity.Id);
            _allInteractables.Remove(interactableViewModel);
        }
    }
    public bool Interact(int Id, string interactableTypeId) => _cmd.Process(new CmdInteract(Id, interactableTypeId));
    public bool PlaceInteractable(string interactableTypeId, Vector3 position, bool isInteractable = true) =>
        _cmd.Process(new CmdPlaceInteractable(interactableTypeId, position, isInteractable));
    public bool RemoveInteractable(int Id) => _cmd.Process(new CmdRemoveInteractable(Id));
}