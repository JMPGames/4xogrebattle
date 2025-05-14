using Godot;

public enum BattleEntityType {
    PLAYER,
    ENEMY,
}

public partial class BattleEntity : Node3D, IDamageable {
    [Export] private int _maxHealth;

    private Vector2I _position;
    private int _health;

    public int MaxHealth => _maxHealth;
    public int Health => _health;
    public bool IsDead => _health <= 0;

    public BattleEntityType BattleEntityType {get; private set;}
    public Vector2I BattlePosition {
        get => _position;
        protected set {
            _position = value;
            // TODO:: Add lerp/tween instead of instant placement
            Position = new Vector3(value.X, 0, value.Y);
        }
    }

    public void Load(int maxHealth, int health) {
        _maxHealth = maxHealth;
        _health = health;
    }

    public void TakeDamage(int amount) {
        _health -= amount;
        if (_health < 0) {
            _health = 0;
        }
    }

    public void Restore(int amount) {
        _health += amount;
        if (_health > _maxHealth) {
            _health = _maxHealth;
        }
    }
}