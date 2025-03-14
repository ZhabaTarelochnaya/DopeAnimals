using System.Threading.Tasks;
using UnityEngine;

public class SettingsProvider : ISettingsProvider
{
    public SettingsProvider()
    {
        ApplicationSettings = Resources.Load<ApplicationSettings>("ApplicationSettings");
    }
    public GameSettings GameSettings { get; private set; }
    public ApplicationSettings ApplicationSettings { get; }

    public Task<GameSettings> LoadGameSettings()
    {
        GameSettings = Resources.Load<GameSettings>("GameSettings");

        return Task.FromResult(GameSettings);
    }
}