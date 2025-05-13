using Godot;

[Tool]
public partial class BoardCreatorInspector : EditorInspectorPlugin {
    public override bool _CanHandle(GodotObject @object) {
        return @object is BoardCreator ? true : false;
    }

    public override void _ParseBegin(GodotObject @object) {
        BoardCreator bc = @object as BoardCreator;

        Button clearButton = new Button {Text = "Clear"};
        clearButton.Pressed += bc.Clear;
        AddCustomControl(clearButton);

        Button rotateButton = new Button {Text = "Rotate Tile"};
        rotateButton.Pressed += bc.Rotate;
        AddCustomControl(rotateButton);

        Button removeTileButton = new Button {Text = "Remove Tile"};
        removeTileButton.Pressed += bc.RemoveTile;
        AddCustomControl(removeTileButton);

        Button upLeftButton = new Button {Text = "Move Up Left"};
        upLeftButton.Pressed += bc.MoveUpLeft;
        AddCustomControl(upLeftButton);

        Button leftButton = new Button {Text = "Move Left"};
        leftButton.Pressed += bc.MoveLeft;
        AddCustomControl(leftButton);

        Button downLeftButton = new Button {Text = "Move Down Left"};
        downLeftButton.Pressed += bc.MoveDownLeft;
        AddCustomControl(downLeftButton);

        Button upRightButton = new Button {Text = "Move Up Right"};
        upRightButton.Pressed += bc.MoveUpRight;
        AddCustomControl(upRightButton);

        Button rightButton = new Button {Text = "Move Right"};
        rightButton.Pressed += bc.MoveRight;
        AddCustomControl(rightButton);

        Button DownRightButton = new Button {Text = "Move Down Right"};
        DownRightButton.Pressed += bc.MoveDownRight;
        AddCustomControl(DownRightButton);

        Button placeGrassButton = new Button {Text = "Place Grass"};
        placeGrassButton.Pressed += bc.PlaceGrass;
        AddCustomControl(placeGrassButton);

        Button placeTreesButton = new Button {Text = "Place Trees"};
        placeTreesButton.Pressed += bc.PlaceTrees;
        AddCustomControl(placeTreesButton);

        Button placeHillButton = new Button {Text = "Place Hill"};
        placeHillButton.Pressed += bc.PlaceHill;
        AddCustomControl(placeHillButton);

        Button placeMountainButton = new Button {Text = "Place Mountain"};
        placeMountainButton.Pressed += bc.PlaceMountain;
        AddCustomControl(placeMountainButton);

        Button placeWaterButton = new Button {Text = "Place Water"};
        placeWaterButton.Pressed += bc.PlaceWater;
        AddCustomControl(placeWaterButton);

        Button placeFarmButton = new Button {Text = "Place Farm"};
        placeFarmButton.Pressed += bc.PlaceFarm;
        AddCustomControl(placeFarmButton);

        Button placeHouseButton = new Button {Text = "Place House"};
        placeHouseButton.Pressed += bc.PlaceHouse;
        AddCustomControl(placeHouseButton);

        Button placeCastleButton = new Button {Text = "Place Castle"};
        placeCastleButton.Pressed += bc.PlaceCastle;
        AddCustomControl(placeCastleButton);

        Button placeDockButton = new Button {Text = "Place Dock"};
        placeDockButton.Pressed += bc.PlaceDock;
        AddCustomControl(placeDockButton);

        Button placeMineButton = new Button {Text = "Place Mine"};
        placeMineButton.Pressed += bc.PlaceMine;
        AddCustomControl(placeMineButton);

        Button placeAnimalsButton = new Button {Text = "Place Animals"};
        placeAnimalsButton.Pressed += bc.PlaceAnimals;
        AddCustomControl(placeAnimalsButton);

        Button placeBlacksmithButton = new Button {Text = "Place Blacksmith"};
        placeBlacksmithButton.Pressed += bc.PlaceBlacksmith;
        AddCustomControl(placeBlacksmithButton);

        Button placeTowerButton = new Button {Text = "Place Tower"};
        placeTowerButton.Pressed += bc.PlaceTower;
        AddCustomControl(placeTowerButton);

        Button placeSandButton = new Button {Text = "Place Sand"};
        placeSandButton.Pressed += bc.PlaceSand;
        AddCustomControl(placeSandButton);

        Button saveButton = new Button {Text = "Save"};
        saveButton.Pressed += bc.Save;
        AddCustomControl(saveButton);

        Button loadButton = new Button {Text = "Load"};
        loadButton.Pressed += bc.Load;
        AddCustomControl(loadButton);
    }
}