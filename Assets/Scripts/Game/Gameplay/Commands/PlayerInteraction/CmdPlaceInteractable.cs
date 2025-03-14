using UnityEngine;

public class CmdPlaceInteractable : ICommand
{
    public CmdPlaceInteractable(string interactableTypeId, Vector3 position, bool isInteractable = true)
    {
        InteractableTypeId = interactableTypeId;
        Position = position;
        IsInteractable = isInteractable;
    }
    public string InteractableTypeId { get; }
    public Vector3 Position { get; }
    public bool IsInteractable { get; }
}