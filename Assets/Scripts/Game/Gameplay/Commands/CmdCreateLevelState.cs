using System;
using UnityEngine;

public class CmdCreateLevelState : ICommand
{
    public readonly int LevelId;
    public CmdCreateLevelState(int levelId)
    {
        LevelId = levelId;
    }
}
