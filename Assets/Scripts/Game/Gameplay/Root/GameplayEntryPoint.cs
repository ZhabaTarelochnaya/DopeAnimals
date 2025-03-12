using BaCon;
using R3;
using System;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] UIGameplayRootBinder _sceneUIRootPrefab;
    public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
    {
        GameplayRegistarations.Register(gameplayContainer, enterParams);
        var gameplayViewModelsContainer = new DIContainer(gameplayContainer);
        GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);

        var uiRoot = gameplayContainer.Resolve<UIRootView>();
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);

        var mainMenuEnterParams = new MainMenuEnterParams();
        var exitParams = new GameplayExitParams(mainMenuEnterParams);
        var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

        return exitToMainMenuSceneSignal;
    }
}
