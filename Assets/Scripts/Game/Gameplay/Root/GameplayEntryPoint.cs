using R3;
using System;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] UIGameplayRootBinder _sceneUIRootPrefab;
    public Observable<GameplayExitParams> Run(UIRootView uiRoot, GameplayEnterParams enterParams)
    {
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);

        var mainMenuExitParams = new MainMenuEnterParams();
        var exitParams = new GameplayExitParams(mainMenuExitParams);
        var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

        return exitToMainMenuSceneSignal;
    }
}
