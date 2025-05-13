using Godot;

public partial class BaseState: State {
    protected GameController _gameController;

    public override void _Ready() {
        _gameController = GetNode("../../") as GameController;
    }

    protected override void AddListeners() {
        _gameController.InputController.CameraMove += CameraMoveInputEvent;
        _gameController.InputController.CameraRotate += CameraRotateInputEvent;
        _gameController.InputController.Move += MoveInputEvent;
        _gameController.InputController.Action += ActionInputEvent;
        _gameController.InputController.Quit += QuitInputEvent;
    }

    protected override void RemoveListeners() {
        _gameController.InputController.CameraMove -= CameraMoveInputEvent;
        _gameController.InputController.CameraRotate -= CameraRotateInputEvent;
        _gameController.InputController.Move -= MoveInputEvent;
        _gameController.InputController.Action -= ActionInputEvent;
        _gameController.InputController.Quit -= QuitInputEvent;
    }

    protected virtual void CameraMoveInputEvent(Vector3 direction) {}
    protected virtual void CameraRotateInputEvent(Vector2 rotation) {}
    protected virtual void MoveInputEvent(Vector2I axialCoordinate) {}
    protected virtual void ActionInputEvent(int button) {}

    protected virtual void QuitInputEvent() {
        GetTree().Quit();
    }
}