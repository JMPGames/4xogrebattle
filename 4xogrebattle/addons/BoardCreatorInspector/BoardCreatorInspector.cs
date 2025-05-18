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
        AddButton("Generate Terrian", bc.GenerateMap, new Color(0, 0, 1));
        AddButton("(SPACE) Rotate Tile", bc.Rotate, new Color(0.2f, 0.2f, 0.2f));
        AddButton("(`) Remove Tile", bc.RemoveTile, new Color(0.6f, 0.2f, 0.2f));

        InspectorKeyInputControl keyInput = new InspectorKeyInputControl {
            CustomMinimumSize = new Vector2(1, 1),
            FocusMode = Control.FocusModeEnum.All,
            MouseFilter = Control.MouseFilterEnum.Ignore,
            boardCreator = bc
        };
        AddCustomControl(keyInput);
        keyInput.CallDeferred("grab_focus");
        AddButton("Refocus Key Inputs", keyInput.GrabFocus, new Color(0.5f, 0.55f, 0.55f));

        AddButton("([) Place Player Troop Spawn", bc.PlacePlayerTroopSpawn, new Color(0.49f, 0.65f, 0.13f));
        AddButton("(]) Place Enemy Troop Spawn", bc.PlaceEnemyTroopSpawn, new Color(0.65f, 0.41f, 0.13f));

        AddButton("(G) Place Grass", bc.PlaceGrass, new Color(0.49f, 0.65f, 0.13f));
        AddButton("(R) Place Rock ", bc.PlaceRock, new Color(0.63f, 0.32f, 0.18f));
        AddButton("(E) Place Sand", bc.PlaceSand, new Color(0.66f, 0.59f, 0.41f));
        AddButton("(F) Place Forest", bc.PlaceForest, new Color(0.25f, 0.46f, 0.02f));
        AddButton("(M) Place Mountain", bc.PlaceMountain, new Color(0.5f, 0.55f, 0.55f));
        AddButton("(Q) Place Water", bc.PlaceWater, new Color(0.2f, 0.4f, 1f));

        AddButton("(Z) Place Archery Range", bc.PlaceArcheryRange, new Color(0.5f, 0.55f, 0.55f));
        AddButton("(B) Place Barracks", bc.PlaceBarrack, new Color(0.5f, 0.55f, 0.55f));
        AddButton("(C) Place Castle", bc.PlaceCastle, new Color(0.5f, 0.55f, 0.55f));
        AddButton("(P) Place Farm", bc.PlaceFarm, new Color(0.49f, 0.65f, 0.13f));
        AddButton("(H) Place House", bc.PlaceHouse, new Color(0.63f, 0.32f, 0.18f));
        AddButton("(L) Place Lumbermill", bc.PlaceLumbermill, new Color(0.5f, 0.55f, 0.55f));
        AddButton("(K) Place Market", bc.PlaceMarket, new Color(0.5f, 0.55f, 0.55f));
        AddButton("(I) Place Mill", bc.PlaceMill, new Color(0.5f, 0.55f, 0.55f));
        AddButton("(N) Place Mine", bc.PlaceMine, new Color(0.5f, 0.55f, 0.55f));
        AddButton("(T) Place Tower", bc.PlaceTower, new Color(0.5f, 0.55f, 0.55f));
        AddButton("(U) Place Water Mill", bc.PlaceWaterMill, new Color(0.2f, 0.4f, 1f));
        AddButton("(O) Place Well", bc.PlaceWell, new Color(0.2f, 0.4f, 1f));

        AddButton("Save", bc.Save, new Color(0.29f, 0.56f, 0.89f));
        AddButton("Load", bc.LoadButton, new Color(0.29f, 0.56f, 0.89f));
    }

    private void AddButton(string label, Action callback, Color color) {
        Button b = new Button {Text = label};
        b.AddThemeStyleboxOverride("normal", new StyleBoxFlat {BgColor = color});
        b.Pressed += callback;
        AddCustomControl(b);
    }
}