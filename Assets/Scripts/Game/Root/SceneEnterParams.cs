public class SceneEnterParams : ISceneParams
{
    public SceneEnterParams(string sceneName)
    {
        SceneName = sceneName;
    }
    public string SceneName { get; set; }
}