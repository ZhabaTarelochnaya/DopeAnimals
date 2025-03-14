using System.Linq;
using R3;
using ObservableCollections;
public class Level
{
    public int Id => Origin.Id;
    public ObservableList<IInteractableEntity> Interactables { get; } = new();
    public LevelState Origin { get; }
    public Level(LevelState levelState)
    {
        Origin = levelState;
        levelState.Interactables.ForEach(i => Interactables.Add(new InteractableEntity(i)));
        Interactables.ObserveAdd().Subscribe(e =>
        {
            var addedInteractableEntity = e.Value;
            levelState.Interactables.Add(addedInteractableEntity.Origin);
        });
        Interactables.ObserveRemove().Subscribe(e =>
        {
            var removedInteractable = e.Value;
            var removedMapState = levelState.Interactables.FirstOrDefault(b => b.Id == removedInteractable.Id);
            levelState.Interactables.Remove(removedMapState);
        });
    }
}
