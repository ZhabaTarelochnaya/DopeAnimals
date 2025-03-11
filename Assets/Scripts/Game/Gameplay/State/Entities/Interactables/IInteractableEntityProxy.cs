using R3;
using UnityEngine;

public interface IInteractableEntityProxy
{
    public IInteractableEntity Origin { get; }
    public ReactiveProperty<bool> IsInteractable { get; }
    public ReactiveProperty<Vector3> Position { get; }
}
