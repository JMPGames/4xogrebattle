using Godot;

public enum TileType {
    GRASS,
    TREES,
    HILL,
    MOUNTAIN,
    WATER,
    FARM,
    HOUSE,
    CASTLE,
    DOCK,
    MINE,
    ANIMALS,
    BLACKSMITH,
    TOWER,
    SAND,
}

[Tool]
public partial class Tile : Node3D {
    public Vector2I axialCoordinate;
    public TileType tileType;

    public void Match() {
        Position = Common.GetHexPositionFromAxial(axialCoordinate);
    }

    public void Load(Vector2I axialCoordinate) {
        this.axialCoordinate = axialCoordinate;
        Match();
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)	{
	}
}
