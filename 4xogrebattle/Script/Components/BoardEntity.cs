using Godot;

public enum BoardEntityType {
    BUILDING,
    TROOP,
}

public partial class BoardEntity: Node3D {
    public Tile Tile {get; protected set;}
    public BoardEntityType BoardEntityType {get; protected set;}

    public virtual void DisplayInfo() {}
}