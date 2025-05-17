using Godot;

public enum BuildingType {
    SECURE,
    UNSECURE,
    ENEMY_BASE,
}

public partial class Building : Node3D {
    [Export] private BuildingType _buildingType;

    protected BoardActionable boardActionable;

    public BuildingType BuildingType => _buildingType;
    public string ActionName => boardActionable?.ActionName ?? "Interact";
    public bool EntityCanInteract(BoardEntity e) => boardActionable?.CanUse(e) ?? false;

    public void Interact(BoardEntity boardEntity) {
        boardActionable?.Action(boardEntity);
    }

    public void CancelInteraction() {
        boardActionable?.Cancel();
    }

    public void ToggleSecureStatus() {
        if (_buildingType == BuildingType.ENEMY_BASE) {
            return;
        }
        _buildingType = _buildingType == BuildingType.SECURE ? BuildingType.UNSECURE : BuildingType.SECURE;
    }

    public void Load(BuildingType buildingType) {
        _buildingType = buildingType;
    }
}