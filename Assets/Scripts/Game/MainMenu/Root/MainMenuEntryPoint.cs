using R3;
using UnityEngine;
public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] UIMainMenuRootBinder _sceneUIRootPrefab;
    public Observable<MainMenuExitParams>Run(UIRootView uiRoot, MainMenuEnterParams enterParams)
    {
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);
        var exitSignalSubject = new Subject<Unit>();
        uiScene.Bind(exitSignalSubject);
        var gameplayEnterParams = new GameplayEnterParams("SaveFile", 1);
        var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
        var exitToGameplaySignal = exitSignalSubject.Select(_ => mainMenuExitParams);
        return exitToGameplaySignal;
    }
}
