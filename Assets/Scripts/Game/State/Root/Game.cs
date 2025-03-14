using System.Linq;
using R3;
using ObservableCollections;
public class Game
{
    GameState _gameState;
    public ReactiveProperty<int> CurrentLevelId = new();
    public ObservableList<Level> Levels { get; } = new();
    public Game(GameState gameState)
    {
        _gameState = gameState;
        gameState.Levels.ForEach(i => Levels.Add(new Level(i)));
        Levels.ObserveAdd().Subscribe(e =>
        {
            var addedLevel = e.Value;
            gameState.Levels.Add(addedLevel.Origin);
        });
        Levels.ObserveRemove().Subscribe(e =>
        {
            var removedLevel = e.Value;
            var removedLevelState = gameState.Levels.FirstOrDefault(b => b.Id == removedLevel.Id);
            gameState.Levels.Remove(removedLevelState);
        });
        CurrentLevelId.Subscribe(v => _gameState.CurrentLevelId = v);
    }
    public int CreateEntityId() => _gameState.CreateEntityId();
}
