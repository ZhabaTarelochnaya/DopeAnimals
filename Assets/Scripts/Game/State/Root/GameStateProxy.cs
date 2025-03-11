using System;
using UnityEngine;
using UnityEngine.Analytics;

public class GameStateProxy
{
    GameState _gameState;
    public GameStateProxy(GameState gameState)
    {
        _gameState = gameState;
    }
    public int GetEntityId() => _gameState.GlobalEntityId++;
}
