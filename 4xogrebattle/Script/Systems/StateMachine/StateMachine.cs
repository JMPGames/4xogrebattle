using Godot;

public partial class StateMachine : Node {
    public State CurrentState {get; protected set;}

    public void ChangeState(State newState) {
        if (CurrentState == newState) {
            return;
        }
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
}