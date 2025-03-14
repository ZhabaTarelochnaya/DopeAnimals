using BaCon;
using R3;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] UIGameplayRootBinder _sceneUIRootPrefab;
    [SerializeField] WorldGameplayRootBinder _worldRootBinder;

    public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
    {
        GameplayRegistrations.Register(gameplayContainer, enterParams);
        var gameplayViewModelsContainer = new DIContainer(gameplayContainer);
        GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);

        // Для теста:
        InitWorld(gameplayViewModelsContainer);
        InitUI(gameplayViewModelsContainer);

        Debug.Log($"GAMEPLAY ENTRY POINT, level to load = {enterParams.LevelId}");

        var mainMenuEnterParams = new MainMenuEnterParams();
        var exitParams = new GameplayExitParams(mainMenuEnterParams);

        var exitSceneSignalSubj = new Subject<Unit>();
        Observable<GameplayExitParams> exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

        return exitToMainMenuSceneSignal;
    }

    void InitWorld(DIContainer viewsContainer)
    {
        _worldRootBinder.Bind(viewsContainer.Resolve<WorldGameplayRootViewModel>());
    }

    void InitUI(DIContainer viewsContainer)
    {
        var uiRoot = viewsContainer.Resolve<UIRootView>();
        UIGameplayRootBinder uiSceneRootBinder = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

        var uiSceneRootViewModel = viewsContainer.Resolve<UIGameplayRootViewModel>();
        uiSceneRootBinder.Bind(uiSceneRootViewModel);
    }
}