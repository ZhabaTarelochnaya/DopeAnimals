using System;
using UnityEngine;
[Serializable]
public class RoomInteractableState
{
    public ECookingState CookingState { get; set; }
    public bool IsInteractable { get; set; }
}
