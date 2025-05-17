using Godot;

[GlobalClass]
public partial class TroopFormation : Node {
    [Signal] public delegate void BattleEntityRemovedEventHandler();

    private readonly Troop _troop;
    private readonly BattleEntity[] _battleEntities;

    public TroopFormation(Troop troop) {
        _troop = troop;
        _battleEntities = new BattleEntity[Common.MAX_ENTITIES_PER_TROOP];
    }

    public BattleEntity[] GetAllBattleEntities => _battleEntities;

    public BattleEntity GetBattleEntity(int i) {
        return Common.IndexAndValueExistsInArray(i, _battleEntities) ? _battleEntities[i] : null;
    }

    public bool SetBattleEntity(int i, BattleEntity battleEntity) {
        if (!Common.IndexExistsInArray(i, _battleEntities)) {
            return false;
        }
        else if (battleEntity == null || _battleEntities[i] != null) {
            return false;
        }
        _battleEntities[i] = battleEntity;
        _troop.MoveMod += battleEntity.Stats.Move / 10;
        return true;
    }

    public bool RemoveBattleEntity(int i) {
        if (Common.IndexExistsInArray(i, _battleEntities)) {
            EmitSignal(SignalName.BattleEntityRemoved, _battleEntities[i]);
            _troop.MoveMod -= _battleEntities[i].Stats.Move / 10;
            _battleEntities[i] = null;
            return true;
        }
        return false;
    }
}