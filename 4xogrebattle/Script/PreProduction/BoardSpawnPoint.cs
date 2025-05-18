using Godot;

public partial class BoardSpawnPoint : Node3D {
    private Vector2I _position;
    public Vector2I Pos => _position;

    public void Load(Vector2I position) {
        _position = position;
        Position = new Vector3(position.X, 1, position.Y);
    }
}