using UnityEngine;

public class UIRootView : MonoBehaviour
{
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] Transform _uiSceneContainer;
    void Awake()
    {
        HideLoadingScreen();
    }
    public void ShowLoagingScreen() => _loadingScreen.SetActive(true);
    public void HideLoadingScreen() => _loadingScreen.SetActive(false);
    public void AttachSceneUI(GameObject sceneUI)
    {
        ClearSceneUI();
        sceneUI.transform.SetParent(_uiSceneContainer, false);
    }

    void ClearSceneUI()
    {
        for (var i = 0; i < _uiSceneContainer.childCount; i++)
        {
            Destroy(_uiSceneContainer.GetChild(i).gameObject);
        }
    }
}