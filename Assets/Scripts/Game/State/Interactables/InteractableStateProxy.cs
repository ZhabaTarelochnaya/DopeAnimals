using R3;
using System;
using UnityEngine;

public class InteractableStateProxy
{
    public EInteractableName InteractableName { get; }
    public ECookingState CookingState { get; }
    public ReactiveProperty<bool> IsInteractable { get; }
    public InteractableStateProxy(InteractableState interactableState)
    {
        InteractableName = interactableState.InteractableName;
        CookingState = interactableState.CookingState;
        IsInteractable = new ReactiveProperty<bool>(interactableState.IsInteractable);

        IsInteractable.Skip(1).Subscribe(s => interactableState.IsInteractable = s);
    }
}
