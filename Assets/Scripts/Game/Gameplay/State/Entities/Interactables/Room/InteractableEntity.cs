using System;
using UnityEngine;
[Serializable]
public class InteractableEntity : IInteractableEntity
{
    public int Id { get; set; }
    public ECookingState CookingState { get; set; }
    public bool IsInteractable { get; set; }
}
