using UnityEngine;

[CreateAssetMenu(fileName = "LevelsSettings", menuName = "Game Settings/New Game Settings")]
public class GameSettings : ScriptableObject
{
    public GameSettings(GameSettingsState gameSettingsState)
    {
        
    }
    public ApplicationSettings ApplicationSettings { get; private set; }
    public InteractablesSettings InteractablesSettings { get; private set; }
    public LevelsSettings LevelsSettings { get; private set; }
}