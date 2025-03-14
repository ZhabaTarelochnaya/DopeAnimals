using System;
using System.Threading.Tasks;
using UnityEngine;

public interface ISettingsProvider
{
    GameSettings GameSettings { get; }
    ApplicationSettings ApplicationSettings { get; }

    Task<GameSettings> LoadGameSettings();
}
