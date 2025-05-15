using Godot;

public partial class BattleStats : Node {
    [Export] private int _maxHealth;
    [Export] private int _physicalAttack;
    [Export] private int _magicAttack;
    [Export] private int _physicalDefense;
    [Export] private int _magicDefense;
    [Export] private int _speed = 1;
    [Export] private int _move = 1;

    public int MaxHealth => _maxHealth;
    public int PhysicalAttack => _physicalAttack;
    public int MagicAttack => _magicAttack;
    public int PhysicalDefense => _physicalDefense;
    public int MagicDefense => _magicAttack;
    public int Speed => _speed;
    public int Move => _move;
}