using Godot;
using System.Linq;
using System.Collections.Generic;

public partial class BattleController : Node {
    private TroopFormation _playerFormation;
    private TroopFormation _enemyFormation;
    private List<BattleEntity> _turnList;

    public TroopFormation PlayerFormation => _playerFormation;
    public TroopFormation EnemyFormation => _enemyFormation;

    public void Setup(TroopFormation playerFormation, TroopFormation enemyFormation) {
        _playerFormation = playerFormation;
        _enemyFormation = enemyFormation;
        _playerFormation.BattleEntityRemoved += BuildTurnList;
        _enemyFormation.BattleEntityRemoved += BuildTurnList;
        BuildTurnList();
    }

    public void BuildTurnList() {
        BattleEntity[] playerEntities = _playerFormation.GetAllBattleEntities;
        BattleEntity[] enemyEntities = _enemyFormation.GetAllBattleEntities;
        _turnList = playerEntities.Concat(enemyEntities).OrderBy(p => p.Stats.Speed).ToList();
    }
}