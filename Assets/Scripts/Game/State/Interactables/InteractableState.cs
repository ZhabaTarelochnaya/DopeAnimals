using System;
using UnityEngine;
[Serializable]
public class InteractableState
{
    public EInteractableName InteractableName {  get; set; }
    public ECookingState CookingState { get; set; }
    public bool IsInteractable { get; set; }
}
