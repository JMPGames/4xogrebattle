using Godot;

public enum BuildingType {
    SECURE,
    UNSECURE,
    ENEMY_BASE,
}

public partial class Building : BoardEntity {
    [Export] private BuildingType _buildingType;

    public BuildingType BuildingType => _buildingType;

    public void ToggleSecureStatus() {
        if (_buildingType == BuildingType.ENEMY_BASE) {
            return;
        }
        _buildingType = _buildingType == BuildingType.SECURE ? BuildingType.UNSECURE : BuildingType.SECURE;
    }

    public void Load(Tile tile, Facing facing, BuildingType buildingType) {
        BoardEntityType = BoardEntityType.BUILDING;
        _buildingType = buildingType;
        LoadLocation(tile, facing);
    }
}