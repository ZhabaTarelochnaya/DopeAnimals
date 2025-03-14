using System;
using UnityEngine;

public class InteractableBinder : MonoBehaviour 
{
    public void Bind(InteractableViewModel viewModel)
    {
        transform.position = viewModel.Position.CurrentValue;
    }
}
