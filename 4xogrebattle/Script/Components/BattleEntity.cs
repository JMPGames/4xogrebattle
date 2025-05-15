using Godot;

public enum BattleEntityType {
    PLAYER,
    ENEMY,
}

public partial class BattleEntity : Node3D {
    public BattleStats Stats {get; private set;}
    public Health Health {get; private set;}
    private Vector2I _position;
    private int _health;

    public bool IsDead => Health.HP <= 0;

    public BattleEntityType BattleEntityType {get; private set;}
    public Vector2I BattlePosition {
        get => _position;
        protected set {
            _position = value;
            Position = new Vector3(value.X, 0, value.Y);
        }
    }

    public virtual void TakeAction() {}

    public override void _Ready() {
        Stats = GetNode<BattleStats>("Stats");
        Health = GetNode<Health>("Health");
        Health.Setup(this);
    }

    public void Load(int health = -1) {
        Health.HP = health == -1 ? Stats.MaxHealth : health;
    }
}