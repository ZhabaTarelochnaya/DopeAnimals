using System.Linq;
using ObservableCollections;
using R3;

public class Game
{
    readonly GameState _gameState;
    public ReactiveProperty<int> CurrentLevelId = new();
    public Game(GameState gameState)
    {
        _gameState = gameState;
        gameState.Levels.ForEach(i => Levels.Add(new Level(i)));
        Levels.ObserveAdd().Subscribe(e =>
        {
            Level addedLevel = e.Value;
            gameState.Levels.Add(addedLevel.Origin);
        });
        Levels.ObserveRemove().Subscribe(e =>
        {
            Level removedLevel = e.Value;
            LevelState removedLevelState = gameState.Levels.FirstOrDefault(b => b.Id == removedLevel.Id);
            gameState.Levels.Remove(removedLevelState);
        });
        CurrentLevelId.Subscribe(v => _gameState.CurrentLevelId = v);
    }
    public ObservableList<Level> Levels { get; } = new();
    public int CreateEntityId() => _gameState.CreateEntityId();
}