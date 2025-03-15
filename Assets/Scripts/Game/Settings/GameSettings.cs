using System;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelsSettings", menuName = "Game Settings/New Game Settings")]
public class GameSettings : ScriptableObject
{
    public long LastUpdatedTime;
    public ApplicationSettings ApplicationSettings;
    public LevelsSettings LevelsSettings;
    public InteractablesSettings InteractablesSettings;
}
