using Godot;
using System.Collections.Generic;

public partial class LoadLevelState: BaseState {
    [Export] private State _stateAfterLoad;
    [Export] private string _mapFileName;

    public override void Enter() {
        base.Enter();
        LoadLevel();
    }

    private void LoadLevel() {
        List<Godot.Collections.Array> prototypePlayerTroops = new()
        {
            new Godot.Collections.Array {"Knight", "Fighter", "", "Cleric", "Archer", ""},
            new Godot.Collections.Array {"Knight", "Fighter", "Knight", "Cleric", "Archer", "Mage"},
            new Godot.Collections.Array {"Knight", "Fighter", "Rogue", "Cleric", "Archer", ""}
        };
        _gameController.BoardCreator.Load(prototypePlayerTroops, _mapFileName);
        GD.Print(_gameController.BoardCreator.PlayerSpawnPoints.Count);
        for (int i = 0; i < _gameController.BoardCreator.PlayerSpawnPoints.Count; i++) {
            Node3D spawnPoint = _gameController.BoardCreator.PlayerSpawnPoints[i];
            GD.Print(spawnPoint);
            Vector2I p = new Vector2I((int)spawnPoint.Position.X, (int)spawnPoint.Position.Z);
            Tile tile = _gameController.BoardCreator.GetTileAtPosition(p);
            GD.Print(tile);
            _gameController.BoardCreator.CreateTroopAtPosition(tile, prototypePlayerTroops[i], Faction.PLAYER);
        }
        // TODO:: Build level configurations system, and load in units
        _gameController.StateMachine.ChangeState(_stateAfterLoad);
    }
}