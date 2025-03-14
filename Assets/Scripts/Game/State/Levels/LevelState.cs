using System.Collections.Generic;

public class LevelState
{
    public int Id { get; set; }
    public List<IInteractableEntityState> Interactables { get; set; }
}
