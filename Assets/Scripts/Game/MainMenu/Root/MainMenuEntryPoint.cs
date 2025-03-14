using R3;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] UIMainMenuRootBinder _sceneUIRootPrefab;
    public Observable<MainMenuExitParams> Run(UIRootView uiRoot, MainMenuEnterParams enterParams)
    {
        UIMainMenuRootBinder uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSignalSubject = new Subject<Unit>();
        uiScene.Bind(exitSignalSubject);

        var gameplayEnterParams = new GameplayEnterParams(0);
        var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
        Observable<MainMenuExitParams> exitToGameplaySignal = exitSignalSubject.Select(_ => mainMenuExitParams);

        return exitToGameplaySignal;
    }
}