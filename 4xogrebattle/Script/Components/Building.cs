using Godot;


public partial class Building : Node3D {
    protected BoardActionable boardActionable;

    public string ActionName => boardActionable?.ActionName ?? "Interact";
    public bool EntityCanInteract(BoardEntity e) => boardActionable?.CanUse(e) ?? false;

    public void Interact(BoardEntity boardEntity) {
        boardActionable?.Action(boardEntity);
    }

    public void CancelInteraction() {
        boardActionable?.Cancel();
    }
}