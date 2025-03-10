using R3;
using System;
using UnityEngine;

public class InteractableStateProxy : IInteractableStateProxy
{
    public ReactiveProperty<bool> IsInteractable { get; }
    public InteractableStateProxy(IInteractableState interactableState)
    {
        IsInteractable = new ReactiveProperty<bool>(interactableState.IsInteractable);

        IsInteractable.Skip(1).Subscribe(s => interactableState.IsInteractable = s);
    }
}
