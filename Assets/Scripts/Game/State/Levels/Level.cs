using System.Linq;
using ObservableCollections;
using R3;

public class Level
{
    public Level(LevelState levelState)
    {
        Origin = levelState;
        levelState.Interactables.ForEach(i => Interactables.Add(new InteractableEntity(i)));
        Interactables.ObserveAdd().Subscribe(e =>
        {
            IInteractableEntity addedInteractableEntity = e.Value;
            levelState.Interactables.Add(addedInteractableEntity.Origin);
        });
        Interactables.ObserveRemove().Subscribe(e =>
        {
            IInteractableEntity removedInteractable = e.Value;
            IInteractableEntityState removedMapState =
                levelState.Interactables.FirstOrDefault(b => b.Id == removedInteractable.Id);
            levelState.Interactables.Remove(removedMapState);
        });
    }
    public int Id => Origin.Id;
    public ObservableList<IInteractableEntity> Interactables { get; } = new();
    public LevelState Origin { get; }
}