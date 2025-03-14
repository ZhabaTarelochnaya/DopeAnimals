using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractablesSettings",
    menuName = "Game Settings/Levels/Interactables/New Interactables Settings")]
public class InteractablesSettings : ScriptableObject
{
    public List<InteractableSettings> Interactables { get; private set; }
}