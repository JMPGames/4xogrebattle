using Godot;

public class MeleeAction : BaseBattleAction {
    protected override BattleEntity CalculateTarget() {
        BattleEntity[] targets = _battleController.EnemyFormation.GetAllBattleEntities;
        BattleEntity target = null;
        for (int i = 0; i < targets.Length; i++) {
            if (targets[i] != null) {
                if (target == null) {
                    target = targets[i];
                }
                else if (targets[i].Health.HP < target.Health.HP) {
                    target = targets[i];
                }
            }
            if (i < 3 && target != null) {
                // Melee can only attack the front row targets until the front row is empty.
                return target;
            }
        }
        return target;
    }

    public override void TakeAction() {
        BattleEntity target = CalculateTarget();
    }
}