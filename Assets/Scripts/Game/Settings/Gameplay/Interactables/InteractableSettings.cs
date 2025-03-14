using System;
using UnityEngine;
[CreateAssetMenu(fileName = "InteractableSettings", menuName = "Game Settings/Levels/Interactables/New Interactable Settings")]
public class InteractableSettings
{
    public LevelInitialStateSettings InitialStateSettings { get; private set; }
}
