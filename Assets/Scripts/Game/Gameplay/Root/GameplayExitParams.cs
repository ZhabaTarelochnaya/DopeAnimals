public class GameplayExitParams
{
    public MainMenuEnterParams MainMenuEnterParams { get; }
    public GameplayExitParams(MainMenuEnterParams mainMenuEntryParams)
    {
        MainMenuEnterParams = mainMenuEntryParams;
    }
}
