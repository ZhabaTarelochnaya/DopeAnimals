
public class GameplayEnterParams : ISceneParams
{
    SceneEnterParams _sceneEnterParams;
    public string SaveFileName { get; }
    public int LevelNumber { get; }
    public string SceneName => SceneNames.Gameplay;

    public GameplayEnterParams(string saveFileName, int levelNumber)
    {
        _sceneEnterParams = new SceneEnterParams(SceneNames.Gameplay);
        SaveFileName = saveFileName;
        LevelNumber = levelNumber;
    }
}
