using System;
using UnityEngine;

public class CmdInteract : ICommand
{
    public void Interact(InteractableEntityProxy interactableStateProxy)
    {
        if (interactableStateProxy.IsInteractable.Value)
        {

        }
    }
}
