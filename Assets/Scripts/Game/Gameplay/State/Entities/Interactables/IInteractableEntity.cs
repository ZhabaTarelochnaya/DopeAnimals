using UnityEngine;
public interface IInteractableEntity : IEntity
{
    public string InteractableTypeID { get; set; }
    public bool IsInteractable { get; set; }
    public Vector3 Position { get; set; }
}
