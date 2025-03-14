using System;
using UnityEngine;

[Serializable]
public class InteractableEntityState : IInteractableEntityState
{
    public int Id { get; set; }
    public bool IsInteractable { get; set; }
    public Vector3 Position { get; set; }
    public string TypeId { get; set; }
}