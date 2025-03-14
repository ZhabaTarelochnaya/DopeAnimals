using UnityEngine;
public interface IInteractableEntityState : IEntity
{
    public string TypeId { get; set; }
    public bool IsInteractable { get; set; }
    public Vector3 Position { get; set; }
}
