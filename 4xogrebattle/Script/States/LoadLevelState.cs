using Godot;

public partial class LoadLevelState: BaseState {
    [Export] private State _stateAfterLoad;
    [Export] private string _mapFileName;

    public override void Enter() {
        base.Enter();
        LoadLevel();
    }

    private void LoadLevel() {
        _gameController.BoardCreator.Load(_mapFileName);
        // TODO:: Build level configurations system, and load in units
        _gameController.StateMachine.ChangeState(_stateAfterLoad);
    }
}