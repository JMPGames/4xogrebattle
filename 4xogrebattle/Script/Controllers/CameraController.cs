using System.Collections.Generic;
using Godot;

public partial class CameraController : Node3D {
    private const float Z_OFFSET = 8.0f;
    private const float EXTRA_PADDING = 8.0f;
    private const float ROTATION_SPEED = 1.0f;    

    [Export] private Camera3D _camera;
    [Export] private float _moveXLimit;
    [Export] private float _moveYLimit;

    private float _yawRotation = 0.0f;
    private Tile _currentHoveredTile;

    public override void _Input(InputEvent @event) {
        if (@event is InputEventMouseMotion) {
            DetectMouseToTile();
        }
        if (@event is InputEventMouseButton mEvent && mEvent.Pressed && mEvent.ButtonIndex == MouseButton.Left) {
            DetectMouseToTile(click: true);
        }
    }

    public void MoveToTile(Vector2I tilePosition) {
        Vector3 targetPosition = new Vector3(tilePosition.X, 0, tilePosition.Y);
        GlobalPosition = targetPosition + new Vector3(0, 0, Z_OFFSET);
        LookAt(targetPosition, Vector3.Up);
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
        position.X = Mathf.Clamp(position.X, 0 - EXTRA_PADDING, _moveXLimit + EXTRA_PADDING);
        position.Z = Mathf.Clamp(position.Z, 0 - EXTRA_PADDING, _moveYLimit + EXTRA_PADDING);
        position.Y = 0;
        Position = position;
    }

    private void DetectMouseToTile(bool click = false) {
        Vector2 mousePosition = GetViewport().GetMousePosition();
        PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
        Vector3 rayOrigin = _camera.ProjectRayOrigin(mousePosition);
        Vector3 rayEnd = rayOrigin + _camera.ProjectRayNormal(mousePosition) * 1000;

        PhysicsRayQueryParameters3D rayQuery = PhysicsRayQueryParameters3D.Create(rayOrigin, rayEnd);
        Godot.Collections.Dictionary result = spaceState.IntersectRay(rayQuery);

        if (result.Count > 0 && result.TryGetValue("collider", out Variant collVariant)) {
            Node collNode = collVariant.As<Node>();
            if (collNode is Tile tile) {
                if (click) {
                    tile.OnClick();
                }
                else if (tile != _currentHoveredTile) {
                    _currentHoveredTile?.OnHoverExit();
                    tile.OnHoverEnter();
                    _currentHoveredTile = tile;
                }
            }
        }
        else {
            _currentHoveredTile?.OnHoverExit();
            _currentHoveredTile = null;
        }
    }
}
