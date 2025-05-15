public partial class Troop : BoardEntity {
    private TroopFormation _formation;
    public TroopFormation Formation => _formation;

    public void Load(Tile tile, Facing facing, Unit[] units) {
        BoardEntityType = BoardEntityType.TROOP;
        _formation = new TroopFormation();
        for (int i = 0; i < units.Length; i++) {
            _formation.SetUnit(i, units[i]);
        }
        LoadLocation(tile, facing);
    }    
}