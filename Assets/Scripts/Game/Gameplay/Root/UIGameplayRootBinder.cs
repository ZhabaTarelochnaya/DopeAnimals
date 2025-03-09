using R3;
using System;
using UnityEngine;

public class UIGameplayRootBinder : MonoBehaviour
{
    Subject<Unit> _exitSceneSignalSubject;
    public void HandleGoToGameplayButtonClick()
    {
        _exitSceneSignalSubject?.OnNext(Unit.Default);
    }
    public void Bind(Subject<Unit> exitSceneSignalSubject)
    {
        _exitSceneSignalSubject = exitSceneSignalSubject;
    }
}
