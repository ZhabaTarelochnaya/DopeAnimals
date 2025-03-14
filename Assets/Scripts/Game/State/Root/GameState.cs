using System;
using System.Collections.Generic;

[Serializable]
public class GameState
{
    public int GlobalEntityId { get; set; }
    public int CurrentLevelId { get; set; }
    public List<LevelState> Levels { get; set; }
    public int CreateEntityId() => GlobalEntityId++;
}
