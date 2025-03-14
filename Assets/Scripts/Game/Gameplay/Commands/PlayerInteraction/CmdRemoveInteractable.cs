public class CmdRemoveInteractable : ICommand
{
    public CmdRemoveInteractable(int id)
    {
        Id = id;
    }
    public int Id { get; }
}