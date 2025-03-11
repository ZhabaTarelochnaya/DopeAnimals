using System;
using UnityEngine;

public class CmdInteract : ICommand
{
    public Vector3 Position { get; }
    public bool IsInteractable { get; }
    public void Interact(InteractableEntityProxy interactableStateProxy)
    {
        if (interactableStateProxy.IsInteractable.Value)
        {
            
        }
    }
}
