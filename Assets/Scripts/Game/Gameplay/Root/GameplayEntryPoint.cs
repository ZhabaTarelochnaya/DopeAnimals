using BaCon;
using R3;
using System;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] UIGameplayRootBinder _sceneUIRootPrefab;
    [SerializeField] private WorldGameplayRootBinder _worldRootBinder;

    public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
    {
        GameplayRegistrations.Register(gameplayContainer, enterParams);
        var gameplayViewModelsContainer = new DIContainer(gameplayContainer);
        GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);

        // Для теста:

        Debug.Log($"GAMEPLAY ENTRY POINT, level to load = {enterParams.LevelId}");

        

        var mainMenuEnterParams = new MainMenuEnterParams();
        var exitParams = new GameplayExitParams(mainMenuEnterParams);

        var exitSceneSignalSubj = new Subject<Unit>();
        var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

        return exitToMainMenuSceneSignal;
    }

    private void InitWorld(DIContainer viewsContainer)
    {
        _worldRootBinder.Bind(viewsContainer.Resolve<WorldGameplayRootViewModel>());
    }

    private void InitUI(DIContainer viewsContainer)
    {
        var uiRoot = viewsContainer.Resolve<UIRootView>();
        var uiSceneRootBinder = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

        var uiSceneRootViewModel = viewsContainer.Resolve<UIGameplayRootViewModel>();
        uiSceneRootBinder.Bind(uiSceneRootViewModel);

        var uiManager = viewsContainer.Resolve<GameplayUIManager>();
        uiManager.OpenScreenGameplay();
    }
}
