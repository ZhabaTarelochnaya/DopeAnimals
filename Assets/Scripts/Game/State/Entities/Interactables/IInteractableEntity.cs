using R3;
using UnityEngine;

public interface IInteractableEntity : IEntityProxy
{
    public IInteractableEntityState Origin { get; }
    public ReactiveProperty<bool> IsInteractable { get; }
    public ReactiveProperty<Vector3> Position { get; }
}
