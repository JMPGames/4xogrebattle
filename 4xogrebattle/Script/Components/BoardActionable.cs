using System;
using Godot;

public partial class BoardActionable : Node {
    public string ActionName;
    public Func<BoardEntity, bool> CanUseFunc;
    public Action<BoardEntity> ActionAction;
    public Action CancelAction;

    public bool CanUse(BoardEntity e) => CanUseFunc?.Invoke(e) ?? false;
    public void Action(BoardEntity e) => ActionAction?.Invoke(e);
    public void Cancel() => CancelAction?.Invoke();
}