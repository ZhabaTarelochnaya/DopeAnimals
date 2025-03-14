using R3;

public interface IGameStateProvider
{
    public Game GameState { get; }
    public GameSettings SettingsState { get; }

    public Observable<Game> LoadGameState();
    public Observable<GameSettings> LoadSettingsState();
    public Observable<bool> SaveGameState();
    public Observable<bool> SaveSettingsState();
    public Observable<bool> ResetGameState();
    public Observable<GameSettings> ResetSettingsState();
}