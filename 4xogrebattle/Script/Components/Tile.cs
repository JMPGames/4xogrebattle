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
    [Export] private Node3D _building;

    private Node3D _troop;
    private Vector2I _position;
    private TileType _tileType;

    public Building Building => HasBuilding ? _building as Building : null;
    public Troop Troop => HasBuilding ? _troop as Troop : null;
    public bool HasBuilding => _building != null;
    public bool HasUnit => _troop != null;
    public Vector2I Pos => _position;
    public TileType TileType => _tileType;
    public Vector3 Center => new Vector3(_position.X, 0, _position.Y);

    public void Load(Vector2I position, TileType tileType) {
        _position = position;
        _tileType = tileType;
        Position = new Vector3(_position.X, 0, _position.Y);
    }

    public void ClearTroop() {
        _troop = null;
    }

    public void SetTroop(Troop troop) {
        _troop = troop;
    }
}
