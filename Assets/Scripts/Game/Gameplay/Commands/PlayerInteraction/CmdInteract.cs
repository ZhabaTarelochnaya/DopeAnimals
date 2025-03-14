public class CmdInteract : ICommand
{
    public CmdInteract(int id, string interactableTypeId)
    {
        Id = id;
    }
    public int Id { get; }
}