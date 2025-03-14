public class GameplayExitParams
{
    public GameplayExitParams(MainMenuEnterParams mainMenuEntryParams)
    {
        MainMenuEnterParams = mainMenuEntryParams;
    }
    public MainMenuEnterParams MainMenuEnterParams { get; }
}