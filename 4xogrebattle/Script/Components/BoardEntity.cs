using Godot;

public partial class BoardEntity: Node3D {
    protected const int BASE_MOVE = 3;

    public Tile Tile { get; protected set; }
    public Faction Faction {get; protected set;}
    public Facing Facing {get; protected set;}

    public virtual void DisplayInfo() {}
    public virtual bool CanMove() => false;

    public void MoveTo(Tile targetTile) {
        if (!CanMove()) {
            return;
        }
        if (Tile?.Entity == this) {
            Tile.ClearEntity();
        }
        // TODO:: Add lerp/tween movement instead of instant placement
        SetFacing(targetTile);
        SetTile(targetTile);
        Match();
    }

    protected void LoadLocation(Tile tile, Facing facing) {
        SetTile(tile);
        Facing = facing;
        Match();
    }

    protected void SetTile(Tile tile) {
        Tile = tile;
        tile?.SetEntity(this);
    }

    private void Match() {
        Position = new Vector3(Tile.Center.X, 1, Tile.Center.Z);
        RotationDegrees = new Vector3(0, (int)Facing * 90, 0);
    }

    protected void SetFacing(Tile targetTile) {
        Vector2I directionToTile = Tile.Pos - targetTile.Pos;
        Facing = Mathf.Abs(directionToTile.X) > Mathf.Abs(directionToTile.Y)
            ? (directionToTile.X < 0 ? Facing.EAST : Facing.WEST)
            : (directionToTile.Y < 0 ? Facing.SOUTH : Facing.NORTH);
    }
}