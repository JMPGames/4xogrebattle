using Godot;

public partial class MarketBuilding : Building {
    // [Export] public Item[] inventory;

    public override void _Ready() {
        //TODO:: Load in items
        boardActionable = new BoardActionable {
            ActionName = "Browse Wares",
            CanUseFunc = boardEntity => CanUse(boardEntity),
            ActionAction = boardEntity => OpenMarketUI(boardEntity),
            CancelAction = () => CloseMarketUI()
        };
    }

    private void OpenMarketUI(BoardEntity boardEntity) {
        if (!CanUse(boardEntity)) {
            return;
        }
        GD.Print("Open Market UI");
    }

    public void CloseMarketUI() {
        GD.Print("Close Market UI");
    }

    private bool CanUse(BoardEntity boardEntity) {
        return boardEntity.Faction == Faction.PLAYER;
    }
}