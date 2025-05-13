using Godot;

public static class Common {
    public const float TILE_RADIUS = 0.578f;

    public static Vector3 GetHexPositionFromAxial(Vector2I axial) {
        float x = TILE_RADIUS * 1.5f * axial.X;
        float z = TILE_RADIUS * Mathf.Sqrt(3.0f) * (axial.Y + axial.X / 2.0f);
        return new Vector3(x, 0, z);
    }
}