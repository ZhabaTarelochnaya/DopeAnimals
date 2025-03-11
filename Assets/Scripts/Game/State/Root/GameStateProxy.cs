
using R3;
using ObservableCollections;
public class GameStateProxy
{
    GameState _gameState;
    public ObservableList<IInteractableEntityProxy> Interactables { get; } = new();
    public GameStateProxy(GameState gameState)
    {
        _gameState = gameState;
        gameState.Interactables.ForEach(i => Interactables.Add(new InteractableEntityProxy(i)));
        Interactables.ObserveAdd().Subscribe(e =>
        {
            var addedInteractableEntity = e.Value;
            gameState.Interactables.Add(addedInteractableEntity.Origin);
        });
    }
    public int GetEntityId() => _gameState.GlobalEntityId++;
}
