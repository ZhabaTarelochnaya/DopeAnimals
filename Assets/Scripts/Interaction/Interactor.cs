using UnityEngine;

public class Interactor
{
    public Transform InteractorSource { get; set; }
    public float InteractRange { get; set; }
    public void Interact()
    {
        var r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (!hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                if (interactable.IsInteractable)
                {
                    interactable.Interact();
                }
            }
        }
    }
}