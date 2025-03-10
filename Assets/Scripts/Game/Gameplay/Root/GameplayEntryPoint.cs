using BaCon;
using R3;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] UIGameplayRootBinder _sceneUIRootPrefab;
    public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
    {
        GameplayRegistrations.Register(gameplayContainer, enterParams);
        var gameplayViewModelContainer = new DIContainer(gameplayContainer);
        GameplayViewModelRegistrations.Register(gameplayViewModelContainer);

        var uiScene = Instantiate(_sceneUIRootPrefab);
        var uiRoot = gameplayContainer.Resolve<UIRootView>();
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);

        var mainMenuExitParams = new MainMenuEnterParams();
        var exitParams = new GameplayExitParams(mainMenuExitParams);
        var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

        return exitToMainMenuSceneSignal;
    }
}
