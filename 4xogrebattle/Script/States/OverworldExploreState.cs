using Godot;

public partial class OverworldExploreState : BaseState {
    protected override void CameraMoveInputEvent(Vector3 direction) {
        _gameController.CameraController.CameraMove(direction);
    }

    protected override void CameraRotateInputEvent(Vector2 rotation) {
        _gameController.CameraController.CameraRotate(rotation);
    }
}