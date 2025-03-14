
public class GameplayEnterParams : ISceneParams
{
    public int LevelId { get; }

    public string SceneName => SceneNames.Gameplay;

    public GameplayEnterParams(int levelId)
    {
        LevelId = levelId;
    }
}
