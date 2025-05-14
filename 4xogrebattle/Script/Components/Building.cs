using Godot;

public enum BuildingType {
    SECURE,
    UNSECURE,
    ENEMY_BASE,
}

public partial class Building : BoardEntity {
    [Export] private BuildingType _buildingType;

    public BuildingType BuildingType {
        get => _buildingType;
        set {
            _buildingType = value;
        }
    }

    public void ToggleSecureStatus() {
        if (_buildingType == BuildingType.ENEMY_BASE) {
            return;
        }
        _buildingType = _buildingType == BuildingType.SECURE ? BuildingType.UNSECURE : BuildingType.SECURE;
    }
}