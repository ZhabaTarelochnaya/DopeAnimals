public class GameplayEnterParams : ISceneParams
{
    public GameplayEnterParams(int levelId)
    {
        LevelId = levelId;
    }
    public int LevelId { get; }

    public string SceneName => SceneNames.Gameplay;
}