public class BaseBattleAction {
    protected BattleController _battleController;
    protected BattleEntity _owner;

    public void Load(BattleController battleController, BattleEntity owner) {
        _battleController = battleController;
        _owner = owner;
    }

    protected virtual BattleEntity CalculateTarget() { return null; }
    public virtual void TakeAction() {}
}