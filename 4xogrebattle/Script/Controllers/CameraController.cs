using Godot;

public partial class CameraController : Node3D {
    private const float ROTATION_SPEED = 1.0f;    

    [Export] private Camera3D _camera;

    private Vector2 _moveLimit = new Vector2(10.0f, 10.0f);
    private float _yawRotation = 0.0f;

    public void Setup(Vector2 moveLimit) {
        _moveLimit = moveLimit;
    }

    public void CameraMove(Vector3 direction) {
        direction.Y = 0;
        Translate(direction);
        ClampPosition();
    }

    public void CameraRotate(Vector2 rotation) {
       _yawRotation -= rotation.X * ROTATION_SPEED;
       Rotation = new Vector3(0, _yawRotation, 0);
    }

    private void ClampPosition() {
        Vector3 position = Position;
        position.X = Mathf.Clamp(position.X, -_moveLimit.X / 2, _moveLimit.X / 2);
        position.Z = Mathf.Clamp(position.Z, -_moveLimit.Y / 2, _moveLimit.Y / 2);
        position.Y = 0;
        Position = position;
    }
}
