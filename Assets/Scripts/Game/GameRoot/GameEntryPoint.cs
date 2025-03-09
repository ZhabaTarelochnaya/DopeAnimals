using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using BaCon;

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
    }
    void RunGame()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == SceneNames.Gameplay)
        {
            _coroutines.StartCoroutine(LoadAndStartGameplay());
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
        _coroutines.StartCoroutine(LoadAndStartGameplay());
     
#endif
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        yield return LoadScene(SceneNames.Boot);
        yield return LoadScene(SceneNames.MainMenu);
        yield return null;
        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
    }

    private IEnumerator LoadAndStartGameplay()
    {
        _uiRoot.ShowLoagingScreen();
        yield return LoadScene(SceneNames.Boot);
        yield return LoadScene(SceneNames.Gameplay);
        yield return null;
        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer);
        sceneEntryPoint.Run(_uiRoot);

        _uiRoot.HideLoadingScreen();
    }
    IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}