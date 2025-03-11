using System;
using UnityEngine;
[Serializable]
public class InteractableEntity : IInteractableEntity
{
    public int Id { get; set; }
    public bool IsInteractable { get; set; }
    public Vector3 Position { get; set; }
}
