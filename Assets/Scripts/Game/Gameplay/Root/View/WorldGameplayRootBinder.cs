using ObservableCollections;
using R3;
using System.Collections.Generic;
using UnityEngine;

public class WorldGameplayRootBinder : MonoBehaviour 
{
    [SerializeField] InteractableBinder _interactableBinder;
    readonly Dictionary<int, InteractableBinder> _createdInteractablesMap;
    readonly CompositeDisposable _disposable = new();


    void OnDestroy() => _disposable.Dispose();

    public void Bind(WorldGameplayRootViewModel viewModel)
    {
        foreach (var interactableViewModel in viewModel.AllInteractables)
        {
            CreateInteractable(interactableViewModel);
        }

        _disposable.Add(viewModel.AllInteractables.ObserveAdd().
            Subscribe(e => CreateInteractable(e.Value)));
        _disposable.Remove(viewModel.AllInteractables.ObserveAdd().
            Subscribe(e => DestroyInteractable(e.Value)));
    }
    void CreateInteractable(InteractableViewModel viewModel)
    {
        var createdInteractable = Instantiate(_interactableBinder);
        createdInteractable.Bind(viewModel);

        _createdInteractablesMap[viewModel.Id] = createdInteractable;
    }
    void DestroyInteractable(InteractableViewModel viewModel)
    {
        if (_createdInteractablesMap.TryGetValue(viewModel.Id, out var interactableBinder))
        {
            Destroy(interactableBinder.gameObject);
            _createdInteractablesMap.Remove(viewModel.Id);
        }
    }
}
