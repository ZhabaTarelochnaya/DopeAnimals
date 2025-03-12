using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerPrefsGameStateProvider : IGameStateProvider
{

    public GameStateProxy GameState { get; private set; }
}
