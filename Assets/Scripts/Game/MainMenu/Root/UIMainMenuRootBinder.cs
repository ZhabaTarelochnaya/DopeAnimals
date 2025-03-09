using System;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    public event Action GoToGameplayButtonClick;

    public void HandleGoToGameplayButtonClick()
    {
        GoToGameplayButtonClick?.Invoke();
    }
}
