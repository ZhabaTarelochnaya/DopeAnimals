
public class GameplayEnterParams : ISceneParams
{
    public int LevelId { get; }

    public GameplayEnterParams(int levelId)
    {
        LevelId = levelId;
    }
}
