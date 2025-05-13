using System.Reflection.Metadata;
using Godot;

[GlobalClass]
public partial class InputController: Node {
    [Signal] public delegate void CameraMoveEventHandler(Vector3 direction);
    [Signal] public delegate void CameraRotateEventHandler(Vector2 rotation);
    [Signal] public delegate void MoveEventHandler(Vector2I axialCoordinate);
    [Signal] public delegate void ActionEventHandler(int button);
    [Signal] public delegate void QuitEventHandler();

    private const float CAMERA_MOVE_SPEED = 5.0f;
    private const float MOUSE_VECTOR_LIMIT = 300.0f;

    private string[] _actionButtons = new string[4] {"select", "cancel", "info", "menu"};
    public string[] ActionButtons {
        get {
            return _actionButtons;
        }
    }
    
    private Vector2 _lastMousePosition;

    public override void _Process(double delta) {
        for (int i = 0; i < _actionButtons.Length; i++) {
            if (Input.IsActionJustPressed(_actionButtons[i])) {
                EmitSignal(SignalName.Action, i);
            }
        }
        HandleCameraInput(delta);
        if (Input.IsActionJustPressed("quit")) {
            EmitSignal(SignalName.Quit);
        }
    }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventMouseMotion mouseEvent && Input.IsActionPressed("camera_rotation_activate")) {
            Vector2 rotation = mouseEvent.Relative / MOUSE_VECTOR_LIMIT;
            EmitSignal(SignalName.CameraRotate, rotation);
        }
    }

    private void HandleCameraInput(double delta) {
        Vector3 moveDirection = Vector3.Zero;
        if (Input.IsActionPressed("camera_forward")) {
            moveDirection.Z -= 1;
        }
        if (Input.IsActionPressed("camera_backward")) {
            moveDirection.Z += 1;
        }
        if (Input.IsActionPressed("camera_left")) {
            moveDirection.X -= 1;
        }
        if (Input.IsActionPressed("camera_right")) {
            moveDirection.X += 1;
        }

        if (moveDirection != Vector3.Zero) {
            EmitSignal(SignalName.CameraMove, moveDirection.Normalized() * CAMERA_MOVE_SPEED * (float)delta);
        }

        float rotationX = Input.GetAxis("camera_rotation_right", "camera_rotation_left");
        if (rotationX != 0) {
            EmitSignal(SignalName.CameraRotate, new Vector2(rotationX, 0));
        }

        if (Input.IsActionJustPressed("camera_rotation_activate")) {
            Vector2 currentMousePosition = GetViewport().GetMousePosition();
            if (_lastMousePosition != Vector2.Zero) {
                Vector2 mouseDelta = currentMousePosition - _lastMousePosition;
                EmitSignal(SignalName.CameraRotate, mouseDelta / MOUSE_VECTOR_LIMIT);
            }
            _lastMousePosition = currentMousePosition;
        }

        if (!Input.IsActionJustPressed("camera_rotation_activate")) {
            _lastMousePosition = Vector2.Zero;
        }
    }
}