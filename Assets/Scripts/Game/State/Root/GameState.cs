using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    public List<IInteractableEntity> Interactables {  get; set; }
    public int GlobalEntityId {  get; set; }
}
