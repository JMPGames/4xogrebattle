using Godot;

public partial class Health : Node {
    public int HP {get; set;}

    private BattleEntity _owner;

    public void Setup(BattleEntity owner) {
        _owner = owner;
    }

    public void TakeDamage(int amount, DamageType damageType) {
        int defense = damageType switch {
            DamageType.PHYSICAL => _owner.Stats.PhysicalDefense,
            DamageType.MAGICAL => _owner.Stats.MagicDefense,
            _ => 0
        };
        float damageReduction = 100.0f / (100.0f + defense);
        int calculatedDamage = Mathf.RoundToInt(amount * damageReduction);
        HP -= Mathf.Max(1, calculatedDamage);
        if (HP < 0) {
            HP = 0;
        }
    }

    public void Heal(int amount) {
        HP += amount;
        if (HP > _owner.Stats.MaxHealth) {
            HP = _owner.Stats.MaxHealth;
        }
    }
}