using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Game Settings/Levels/New Level Settings")]
public class LevelSettings : ScriptableObject
{
    public int Id { get; private set; }
    public LevelInitialStateSettings InitialStateSettings { get; private set; }
}