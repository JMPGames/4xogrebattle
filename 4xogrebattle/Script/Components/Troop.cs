using Godot;

public enum TroopType {
    PLAYER,
    ENEMY,
    NEUTRAL,
}

public enum Facing {
    NORTH,
    EAST,
    SOUTH,
    WEST,
}

public partial class Troop : BoardEntity {
    public Facing Facing {get; protected set;}
    private TroopType _troopType;
    private TroopFormation _formation;

    public TroopType TroopType => _troopType;
    public TroopFormation Formation => _formation;

    private void Load(Tile tile, Facing facing, TroopType troopType, Unit[] units) {
        Tile = tile;
        Tile?.SetTroop(this);
        Facing = facing;
        Match();
        BoardEntityType = BoardEntityType.TROOP;
        _troopType = troopType;
        _formation = new TroopFormation();
        for (int i = 0; i < units.Length; i++) {
            _formation.SetUnit(i, units[i]);
        }
    }

    public void Place(Tile targetTile) {
        if (Tile?.Troop == this) {
            Tile.ClearTroop();
        }
        Tile = targetTile;
        Tile?.SetTroop(this);
        SetFacing(targetTile);
        Match();
    }

    public void Match() {
        Position = Tile.Center;
        RotationDegrees = FacingToEuler();
    }

    protected void SetFacing(Tile targetTile) {
        Vector2I directionToTile = Tile.Pos - targetTile.Pos;
        Facing = Mathf.Abs(directionToTile.X) > Mathf.Abs(directionToTile.Y)
            ? (directionToTile.X < 0 ? Facing.EAST : Facing.WEST)
            : (directionToTile.Y < 0 ? Facing.SOUTH : Facing.NORTH);
    }

    protected Vector3 FacingToEuler() {
        return new Vector3(0, (int)Facing * 90, 0);
    }
}