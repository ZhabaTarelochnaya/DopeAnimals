using R3;
using System;
using UnityEngine;
using static UnityEngine.UI.Image;

public class InteractableEntityProxy : IInteractableEntityProxy
{
    public int Id { get; }
    public IInteractableEntity Origin { get; }
    public ReactiveProperty<bool> IsInteractable { get; }

    public ReactiveProperty<Vector3> Position { get; }

    public InteractableEntityProxy(IInteractableEntity interactableState)
    {
        Origin = interactableState;
        Id = interactableState.Id;
        IsInteractable = new ReactiveProperty<bool>(interactableState.IsInteractable);
        Position = new ReactiveProperty<Vector3>(interactableState.Position);

        IsInteractable.Skip(1).Subscribe(e => interactableState.IsInteractable = e);
        Position.Skip(1).Subscribe(e => interactableState.Position = e);
    }
}
