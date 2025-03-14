using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelsSettings", menuName = "Game Settings/Levels/New Levels Settings")]
public class LevelsSettings : ScriptableObject
{
    public List<LevelSettings> Levels {  get; private set; }
}
