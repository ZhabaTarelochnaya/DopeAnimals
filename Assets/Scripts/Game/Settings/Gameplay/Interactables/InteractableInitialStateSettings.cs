using System;
using UnityEngine;
[Serializable]
public class InteractableInitialStateSettings
{
    public bool IsInteractable { get; set; }
    public string TypeId { get; set; }
    public Vector3 Position { get; set; }
}