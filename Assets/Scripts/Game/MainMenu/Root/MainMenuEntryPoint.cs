using System;
using UnityEngine;
using static UnityEngine.Object;
public class MainMenuEntryPoint : MonoBehaviour
{
    public event Action GoToGamePlayRequested;
    [SerializeField] UIMainMenuRootBinder _sceneUIRootPrefab;
    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);
        uiScene.GoToGameplayButtonClick += () =>
        {
            GoToGamePlayRequested?.Invoke();
        };

    }
}
