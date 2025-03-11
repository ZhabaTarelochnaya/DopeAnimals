using R3;
using System;
using UnityEngine;

public class InteractableEntityProxy : IInteractableEntityProxy
{
    public ReactiveProperty<bool> IsInteractable { get; }
    public InteractableEntityProxy(IInteractableEntity interactableState)
    {
        IsInteractable = new ReactiveProperty<bool>(interactableState.IsInteractable);

        IsInteractable.Skip(1).Subscribe(s => interactableState.IsInteractable = s);
    }
}
