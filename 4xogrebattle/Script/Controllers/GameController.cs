using Godot;

public partial class GameController : Node {
    [Export] private DataManager _dataManager;
    [Export] private BoardCreator _board;
    [Export] private InputController _inputController;
    [Export] private CameraController _cameraController;
    [Export] private StateMachine _stateMachine;
    [Export] private State _initialState;

    public DataManager DataManager => _dataManager;
    public BoardCreator BoardCreator => _board;
    public InputController InputController => _inputController;
    public StateMachine StateMachine => _stateMachine;
    public CameraController CameraController => _cameraController;
    public Tile CurrentTile => _board.GetTileAtPosition();

    public override void _Ready() {
        _dataManager.LoadGameData();
        _stateMachine.ChangeState(_initialState);
    }
}