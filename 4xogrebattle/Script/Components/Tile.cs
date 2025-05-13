using Godot;

public enum TileType {
    GRASS,
    ROCK,
    SAND,
    FOREST,
    MOUNTAIN,
    WATER,
    ARCHERY_RANGE,
    BARRACK,
    CASTLE,
    FARM,
    HOUSE,
    LUMBERMILL,
    MARKET,
    MILL,
    MINE,
    TOWER,
    WATER_MILL,
    WELL,
}

[Tool]
public partial class Tile : Node3D {
    [Export] private Node _content;

    public Vector2I position;
    public TileType tileType;

    public void Match() {
        Position = new Vector3(position.X, 0, position.Y);
    }

    public void Load(Vector2I position) {
        this.position = position;
        Match();
    }
}
