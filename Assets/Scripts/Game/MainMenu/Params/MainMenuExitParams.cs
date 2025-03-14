public class MainMenuExitParams
{
    public MainMenuExitParams(ISceneParams targetSceneEnterParams)
    {
        TargetSceneEnterParams = targetSceneEnterParams;
    }
    public ISceneParams TargetSceneEnterParams { get; }
}