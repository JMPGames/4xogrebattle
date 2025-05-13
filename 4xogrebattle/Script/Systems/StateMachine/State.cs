using Godot;

public partial class State : Node {
    public virtual void Enter() {
        AddListeners();
    }

    public virtual void Exit() {
        RemoveListeners();
    }

    public override void _ExitTree() {
        RemoveListeners();
    }

    protected virtual void AddListeners() {}
    protected virtual void RemoveListeners() {}
}