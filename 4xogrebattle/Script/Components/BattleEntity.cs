using Godot;

public enum BattleEntityType {
    PLAYER,
    ENEMY,
}

public partial class BattleEntity : Node3D {
    [Export] private string _unitName;
    public string UnitName => _unitName;

    private Troop _troop;

    public BattleStats Stats {get; private set;}
    public Health Health {get; private set;}
    public BattleJob Job {get; protected set;}
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

    public virtual BattleJob BattleJob => BattleJob.FIGHTER;
    public virtual void TakeAction() {}

    public override void _Ready() {
        Stats = GetNode<BattleStats>("Stats");
        Health = GetNode<Health>("Health");
        Health.Setup(this);
    }

    public void Load(Troop troop, int health = -1) {
        _troop = troop;
        Health.HP = health == -1 ? Stats.MaxHealth : health;
    }
}