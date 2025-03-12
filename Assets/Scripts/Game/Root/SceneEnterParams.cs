public class SceneEnterParams : ISceneParams
{
    public string SceneName { get; set; }
    public SceneEnterParams(string sceneName)
    {
        SceneName = sceneName;
    }
}
