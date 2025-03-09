public class MainMenuExitParams
{
    public ISceneParams TargetSceneEnterParams { get; }
    public MainMenuExitParams(ISceneParams targetSceneEnterParams)
    {
        TargetSceneEnterParams = targetSceneEnterParams;
    }
}
