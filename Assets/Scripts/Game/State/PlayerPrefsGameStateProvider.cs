using Mono.Cecil;
using R3;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerPrefsGameStateProvider : IGameStateProvider
{
    private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
    private const string GAME_SETTINGS_STATE_KEY = nameof(GAME_SETTINGS_STATE_KEY);

    public Game GameState { get; private set; }
    public GameSettings SettingsState { get; private set; }

    private GameState _gameStateOrigin;
    private GameSettingsState _gameSettingsStateOrigin;

    public Observable<Game> LoadGameState()
    {
        if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
        {
            GameState = CreateGameStateFromSettings();
            Debug.Log("Game State created from settings: " + JsonUtility.ToJson(_gameStateOrigin, true));
            SaveGameState();
        }
        else
        {
            var json = PlayerPrefs.GetString(GAME_STATE_KEY);
            _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
            GameState = new Game(_gameStateOrigin);

            Debug.Log("Game State loaded: " + json);
        }

        return Observable.Return(GameState);
    }

    public Observable<GameSettings> LoadSettingsState()
    {
        if (!PlayerPrefs.HasKey(GAME_SETTINGS_STATE_KEY))
        {
            SettingsState = CreateGameSettingsStateFromSettings();
            SaveSettingsState();
        }
        else
        {
            var json = PlayerPrefs.GetString(GAME_SETTINGS_STATE_KEY);
            _gameSettingsStateOrigin = JsonUtility.FromJson<GameSettingsState>(json);
            SettingsState = new GameSettings(_gameSettingsStateOrigin);
        }

        return Observable.Return(SettingsState);
    }

    public Observable<bool> SaveGameState()
    {
        var json = JsonUtility.ToJson(_gameStateOrigin, true);
        PlayerPrefs.SetString(GAME_STATE_KEY, json);

        return Observable.Return(true);
    }

    public Observable<bool> SaveSettingsState()
    {
        var json = JsonUtility.ToJson(_gameSettingsStateOrigin, true);
        PlayerPrefs.SetString(GAME_SETTINGS_STATE_KEY, json);

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

    private Game CreateGameStateFromSettings()
    {
        // Settings needed
        _gameStateOrigin = new GameState();
        return new Game(_gameStateOrigin);
    }

    private GameSettings CreateGameSettingsStateFromSettings()
    {
        // Состояние по умолчанию из настроек, мы делаем фейк
        _gameSettingsStateOrigin = new GameSettingsState()
        {
            MusicVolume = 8,
            SFXVolume = 8
        };

        return new GameSettings(_gameSettingsStateOrigin);
    }
}