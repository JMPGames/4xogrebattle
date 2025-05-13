using Godot;

public partial class InspectorKeyInputControl : Control {
    public BoardCreator boardCreator;

    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventKey key && key.Pressed) {
            switch (key.Keycode) {
                case Key.W:
                case Key.Up:
                    boardCreator?.MoveUp();
                    break;

                case Key.A:
                case Key.Left:
                    boardCreator?.MoveLeft();
                    break;

                case Key.S:
                case Key.Down:
                    boardCreator?.MoveDown();
                    break;

                case Key.D:
                case Key.Right:
                    boardCreator?.MoveRight();
                    break;

                case Key.G:
                    boardCreator?.PlaceGrass();
                    break;
                case Key.R:
                    boardCreator?.PlaceRock();
                    break;
                case Key.E:
                    boardCreator?.PlaceSand();
                    break;
                case Key.F:
                    boardCreator?.PlaceForest();
                    break;
                case Key.M:
                    boardCreator?.PlaceMountain();
                    break;
                case Key.Q:
                    boardCreator?.PlaceWater();
                    break;

                case Key.Z:
                    boardCreator?.PlaceArcheryRange();
                    break;
                case Key.B:
                    boardCreator?.PlaceBarrack();
                    break;
                case Key.C:
                    boardCreator?.PlaceCastle();
                    break;
                case Key.P:
                    boardCreator?.PlaceFarm();
                    break;
                case Key.H:
                    boardCreator?.PlaceHouse();
                    break;
                case Key.L:
                    boardCreator?.PlaceLumbermill();
                    break;
                case Key.K:
                    boardCreator?.PlaceMarket();
                    break;
                case Key.I:
                    boardCreator?.PlaceMill();
                    break;
                case Key.N:
                    boardCreator?.PlaceMine();
                    break;
                case Key.T:
                    boardCreator?.PlaceTower();
                    break;
                case Key.U:
                    boardCreator?.PlaceWaterMill();
                    break;
                case Key.O:
                    boardCreator?.PlaceWell();
                    break;
            }
        }
    }
}