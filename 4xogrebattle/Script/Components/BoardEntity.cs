using Godot;

public enum BoardEntityType {
    BUILDING,
    TROOP,
}

public partial class BoardEntity: Node3D {
    public Tile Tile {get; protected set;}
    public BoardEntityType BoardEntityType {get; protected set;}
    public Faction Faction {get; protected set;}
    public Facing Facing {get; protected set;}

    public virtual void DisplayInfo() {}

    public void MoveTo(Tile targetTile) {
        if (BoardEntityType == BoardEntityType.BUILDING) {
            return;
        }
        if (Tile?.Entity == this) {
            Tile.ClearEntity();
        }
        // TODO:: Add lerp/tween movement instead of instant placement
        SetFacing(targetTile);
        Tile = targetTile;
        Tile?.SetEntity(this);
        Match();
    }

    protected void LoadLocation(Tile tile, Facing facing) {
        Tile = tile;
        Tile?.SetEntity(this);
        Facing = facing;
        Match();
    }

    private void Match() {
        Position = Tile.Center;
        RotationDegrees = new Vector3(0, (int)Facing * 90, 0);
    }

    protected void SetFacing(Tile targetTile) {
        Vector2I directionToTile = Tile.Pos - targetTile.Pos;
        Facing = Mathf.Abs(directionToTile.X) > Mathf.Abs(directionToTile.Y)
            ? (directionToTile.X < 0 ? Facing.EAST : Facing.WEST)
            : (directionToTile.Y < 0 ? Facing.SOUTH : Facing.NORTH);
    }
}