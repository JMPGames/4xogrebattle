using Godot;

public partial class Unit : BattleEntity {
    [Export] private string _unitName;

    public string UnitName => _unitName;
}