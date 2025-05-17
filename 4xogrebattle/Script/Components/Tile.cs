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

    private Node3D _entity;
    private Vector2I _position;
    private TileType _tileType;

    public Building Building => HasBuilding ? _building as Building : null;
    public BoardEntity Entity => HasEntity ? _entity as BoardEntity : null;
    public bool HasBuilding => _building != null;
    public bool HasEntity => _entity != null;
    public Vector2I Pos => _position;
    public TileType TileType => _tileType;
    public Vector3 Center => new Vector3(_position.X, 0, _position.Y);

    public void Load(Vector2I position, TileType tileType) {
        _position = position;
        _tileType = tileType;
        Position = new Vector3(_position.X, 0, _position.Y);
    }

    public void ClearEntity() {
        _entity = null;
    }

    public void SetEntity(BoardEntity entity) {
        _entity = entity;
    }

    public void OnHoverEnter() {
    }

    public void OnHoverExit() {
    }

    public void OnClick() {
        GD.Print($"Tile clicked at: {_position}---------");
        GD.Print($"Entity Name: {Entity?.Name}");
        GD.Print($"Building Name: {Building?.Name}");
        GD.Print("--------------------------------");
    }
}
