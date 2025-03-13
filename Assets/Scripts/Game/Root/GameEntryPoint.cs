using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using BaCon;
using R3;

public class GameEntryPoint
{
    static GameEntryPoint _instance;
    Coroutines _coroutines;
    UIRootView _uiRoot;
    readonly DIContainer _rootContainer = new();
    DIContainer _cachedSceneContainer;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutostartGame()
    {
        _instance = new GameEntryPoint();
        var sceneEnterParams = new SceneEnterParams(SceneNames.Gameplay);
        _instance.RunGame();
    }
    GameEntryPoint()
    {
        _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(_coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("Prefabs/UIRoot");
        _uiRoot = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(_uiRoot.gameObject);
        _rootContainer.RegisterInstance(_uiRoot);

        var gameStateProvider = new PlayerPrefsGameStateProvider();
        _rootContainer.RegisterInstance<IGameStateProvider>(gameStateProvider);
    }
    void RunGame()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == SceneNames.Gameplay)
        {
            var enterParams = new GameplayEnterParams("SaveFile", 1);
            _coroutines.StartCoroutine(LoadAndStartGameplay(enterParams));
            return;
        }
        if (sceneName == SceneNames.MainMenu)
        {
            _coroutines.StartCoroutine(LoadAndStartMainMenu());
            return;
        }
        if (sceneName != SceneNames.Boot)
        {
            return;
        }
#endif
        _coroutines.StartCoroutine(LoadAndStartMainMenu());

    }

    private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null)
    {
        yield return LoadScene(SceneNames.Boot);
        yield return LoadScene(SceneNames.MainMenu);
        yield return null;
        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(_uiRoot, enterParams).Subscribe(mainMenuExitParams =>
        {
            var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;
            if (targetSceneName == SceneNames.Gameplay)
            {
                _coroutines.StartCoroutine(LoadAndStartGameplay(
                    (GameplayEnterParams)mainMenuExitParams.TargetSceneEnterParams));
            }
        });
    }

    private IEnumerator LoadAndStartGameplay(GameplayEnterParams enterParams)
    {
        _uiRoot.ShowLoagingScreen();
        _cachedSceneContainer?.Dispose();

        yield return LoadScene(SceneNames.Boot);
        yield return LoadScene(SceneNames.Gameplay);
        yield return null;

        var isGameStateLoaded = false;
        _rootContainer.Resolve<IGameStateProvider>().
            LoadGameState().Subscribe(_ => isGameStateLoaded = true);
        yield return new WaitUntil(() => isGameStateLoaded);

        var gameplayEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer);
        gameplayEntryPoint.Run(gameplayContainer, enterParams).Subscribe(gameplayExitParams =>
        {
            _coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEnterParams));
        });

        _uiRoot.HideLoadingScreen();
    }
    IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}