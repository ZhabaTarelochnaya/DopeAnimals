using System;
using UnityEngine;

public class AyahuascaPlant : MonoBehaviour, IInteractable
{
    [SerializeField] Item _ayahuasakaLeaf;
    public bool IsInteractable => throw new NotImplementedException();

    public void Interact()
    {
        throw new NotImplementedException();
    }
}