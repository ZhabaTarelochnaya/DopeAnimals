using UnityEngine;

public class UIGameplayRootBinder : MonoBehaviour
{
    UIGameplayRootViewModel _viewModel;
    public void HandleGoToGameplayButtonClick()
    {
        //_exitSceneSignalSubject?.OnNext(Unit.Default);
    }
    public void Bind(UIGameplayRootViewModel viewModel)
    {
        _viewModel = viewModel;
    }
}