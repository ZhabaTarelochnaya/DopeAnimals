using Mono.Cecil;
using R3;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using UnityEngine;

public class GameStateProvider : IGameStateProvider
{
    public Game GameState { get; private set; }
    public GameSettings SettingsState { get; private set; }

    GameState _gameStateOrigin;
    GameSettings _gameSettingsStateOrigin;
    readonly FileDataHandler _fileDataHandler;
    public GameStateProvider(FileDataHandler fileDataHandler)
    {
        _fileDataHandler = fileDataHandler;
    }
    public Observable<Game> LoadGameState()
    {
        if (!PlayerPrefs.HasKey(GameStateKey))
        {
            GameState = CreateGameStateFromSettings();
            Debug.Log("Game State created from settings: " + JsonUtility.ToJson(_gameStateOrigin, true));
            SaveGameState();
        }
        else
        {
            var json = PlayerPrefs.GetString(GameStateKey);
            _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
            GameState = new Game(_gameStateOrigin);

            Debug.Log("Game State loaded: " + json);
        }

        return Observable.Return(GameState);
    }

    public Observable<GameSettings> LoadSettingsState()
    {
        if (!PlayerPrefs.HasKey(GameSettingsStateKey))
        {
            SettingsState = CreateGameSettingsStateFromSettings();
            SaveSettingsState();
        }
        else
        {
            var json = PlayerPrefs.GetString(GameSettingsStateKey);
            _gameSettingsStateOrigin = JsonUtility.FromJson<GameSettingsState>(json);
            SettingsState = GameSettings();
        }

        return Observable.Return(SettingsState);
    }

    public Observable<bool> SaveGameState()
    {
        var json = JsonUtility.ToJson(_gameStateOrigin, true);
        PlayerPrefs.SetString(GameStateKey, json);

        return Observable.Return(true);
    }

    public Observable<bool> SaveSettingsState()
    {
        var json = JsonUtility.ToJson(_gameSettingsStateOrigin, true);
        PlayerPrefs.SetString(GameSettingsStateKey, json);

        return Observable.Return(true);
    }

    public Observable<bool> ResetGameState()
    {
        GameState = CreateGameStateFromSettings();
        SaveGameState();

        return Observable.Return(true);
    }

    public Observable<GameSettings> ResetSettingsState()
    {
        SettingsState = CreateGameSettingsStateFromSettings();
        SaveSettingsState();

        return Observable.Return(SettingsState);
    }

    Game CreateGameStateFromSettings()
    {
        // Settings needed
        _gameStateOrigin = new GameState();
        return new Game(_gameStateOrigin);
    }

    GameSettings CreateGameSettingsStateFromSettings()
    {
        
        return new GameSettings();
    }
}