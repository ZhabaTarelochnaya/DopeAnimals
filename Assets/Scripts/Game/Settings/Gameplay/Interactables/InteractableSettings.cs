using UnityEngine;

[CreateAssetMenu(fileName = "InteractableSettings",
    menuName = "Game Settings/Levels/Interactables/New Interactable Settings")]
public class InteractableSettings : ScriptableObject
{
    public LevelInitialStateSettings InitialStateSettings { get; private set; }
}