using R3;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsGameStateProvider : IGameStateProvider
{
    const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
    const string GAME_SETTINGS_STATE_KEY = nameof(GAME_SETTINGS_STATE_KEY);

    GameState _gameStateOrigin;
    public GameStateProxy GameState { get; private set; }

    public Observable<GameStateProxy> LoadGameState()
    {
        if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
        {
            GameState = CreateGameStateFromSettings();
            Debug.Log("Game State created from settings: " + JsonUtility.ToJson(_gameStateOrigin, true));

            SaveGameState();    // Сохраним дефолтное состояние
        }
        else
        {
            // Загружаем
            var json = PlayerPrefs.GetString(GAME_STATE_KEY);
            _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
            GameState = new GameStateProxy(_gameStateOrigin);

            Debug.Log("Game State loaded: " + json);
        }

        return Observable.Return(GameState);
    }
    public Observable<bool> SaveGameState()
    {
        var json = JsonUtility.ToJson(_gameStateOrigin, true);
        PlayerPrefs.SetString(GAME_STATE_KEY, json);

        return Observable.Return(true);
    }

    public Observable<bool> ResetGameState()
    {
        GameState = CreateGameStateFromSettings();
        SaveGameState();

        return Observable.Return(true);
    }

    private GameStateProxy CreateGameStateFromSettings()
    {
        // Settings needed
        _gameStateOrigin = new GameState();
        return new GameStateProxy(_gameStateOrigin);
    }
}
