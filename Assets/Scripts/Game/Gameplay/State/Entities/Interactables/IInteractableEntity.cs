
using UnityEngine;

public interface IInteractableEntity : IEntity
{
    public bool IsInteractable { get; set; }
    public Vector3 Position { get; set; }
}
