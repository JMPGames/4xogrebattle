using System;
using Godot;

[Tool]
public partial class BoardCreatorInspector : EditorInspectorPlugin {
    public override bool _CanHandle(GodotObject @object) {
        return @object is BoardCreator ? true : false;
    }

    public override void _ParseBegin(GodotObject @object) {
        BoardCreator bc = @object as BoardCreator;

        AddButton("Clear", bc.Clear, new Color(1, 0, 0));
        AddButton("Rotate Tile", bc.Rotate, new Color(0.2f, 0.2f, 0.2f));
        AddButton("Remove Tile", bc.RemoveTile, new Color(0.6f, 0.2f, 0.2f));

        AddButton("Move Up Left", bc.MoveUpLeft, new Color(0, 0, 0));
        AddButton("Move Up Right", bc.MoveUpRight, new Color(0, 0, 0));
        AddButton("Move Left", bc.MoveLeft, new Color(0, 0, 0));
        AddButton("Move Right", bc.MoveRight, new Color(0, 0, 0));
        AddButton("Move Down Left", bc.MoveDownLeft, new Color(0, 0, 0));
        AddButton("Move Down Right", bc.MoveDownRight, new Color(0, 0, 0));

        AddButton("Place Grass", bc.PlaceGrass, new Color(0.49f, 0.65f, 0.13f));
        AddButton("Place Trees", bc.PlaceTrees, new Color(0.25f, 0.46f, 0.02f));
        AddButton("Place Hill", bc.PlaceHill, new Color(0.63f, 0.32f, 0.18f));
        AddButton("Place Mountain", bc.PlaceMountain, new Color(0.5f, 0.55f, 0.55f));
        AddButton("Place Water", bc.PlaceWater, new Color(0.2f, 0.4f, 1f));
        AddButton("Place Sand", bc.PlaceSand, new Color(0.66f, 0.59f, 0.41f));

        AddButton("Place Farm", bc.PlaceFarm, new Color(0.49f, 0.65f, 0.13f));
        AddButton("Place House", bc.PlaceHouse, new Color(0.63f, 0.32f, 0.18f));
        AddButton("Place Castle", bc.PlaceCastle, new Color(0.5f, 0.55f, 0.55f));
        AddButton("Place Dock", bc.PlaceDock, new Color(0.2f, 0.4f, 1f));
        AddButton("Place Mine", bc.PlaceMine, new Color(0.5f, 0.55f, 0.55f));
        AddButton("Place Blacksmith", bc.PlaceBlacksmith, new Color(0.5f, 0.55f, 0.55f));
        AddButton("Place Tower", bc.PlaceTower, new Color(0.5f, 0.55f, 0.55f));
        AddButton("Place Animals", bc.PlaceAnimals, new Color(0.63f, 0.32f, 0.18f));

        AddButton("Save", bc.Save, new Color(0.29f, 0.56f, 0.89f));
        AddButton("Load", bc.Load, new Color(0.29f, 0.56f, 0.89f));
    }

    private void AddButton(string label, Action callback, Color color) {
        Button b = new Button {Text = label};
        b.AddThemeStyleboxOverride("normal", new StyleBoxFlat {BgColor = color});
        b.Pressed += callback;
        AddCustomControl(b);
    }
}