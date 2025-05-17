using Godot;

public partial class Troop : BoardEntity {
    private TroopFormation _formation;
    public TroopFormation Formation => _formation;

    public float MoveMod {get; set;}

    public override bool CanMove() => true;
    public int Move => Mathf.FloorToInt(BASE_MOVE + MoveMod);

    public override void _Ready() {
        _formation = new TroopFormation(this);
    }

    public void Load(Tile tile, Facing facing, BattleEntity[] battleEntities) {
        for (int i = 0; i < battleEntities.Length; i++) {
            _formation.SetBattleEntity(i, battleEntities[i]);
        }
        LoadLocation(tile, facing);
    }    
}