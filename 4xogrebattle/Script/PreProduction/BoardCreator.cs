using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class BoardCreator : Node {
    private const string SAVE_PATH = "res://data/levels/";
    private const string TILE_PREFAB_PATH = "res://prefabs/tiles/";
    private const float ROTATION_PER = 90.0f;
    private const string DEFAULT_FILE_NAME = "default";

    [Export] private int _width;
    [Export] public string fileName = DEFAULT_FILE_NAME;

    private Node3D _marker;
    private Vector2I _markerPosition;
    private Dictionary<Vector2I, Tile> tiles = new Dictionary<Vector2I, Tile>();

    private PackedScene tileSelectorPrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "TileSelector.tscn");
    private PackedScene grassTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "GrassTile.tscn");
    private PackedScene rockTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "RockTile.tscn");
    private PackedScene sandTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "SandTile.tscn");
    private PackedScene forestTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "ForestTile.tscn");
    private PackedScene mountainTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "MountainTile.tscn");
    private PackedScene waterTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "WaterTile.tscn");
    private PackedScene archeryRangeTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "ArcheryRangeTile.tscn");
    private PackedScene barrackTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "BarrackTile.tscn");
    private PackedScene castleTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "CastleTile.tscn");
    private PackedScene farmTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "FarmTile.tscn");
    private PackedScene houseTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "HouseTile.tscn");
    private PackedScene lumbermillTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "LumbermillTile.tscn");
    private PackedScene marketTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "MarketTile.tscn");
    private PackedScene millTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "MillTile.tscn");
    private PackedScene mineTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "MineTile.tscn");
    private PackedScene towerTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "TowerTile.tscn");
    private PackedScene waterMillTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "WaterMillTile.tscn");
    private PackedScene wellTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "WellTile.tscn");

    public override void _Ready() {
        SetProcessInput(true);
        _marker = tileSelectorPrefab.Instantiate() as Node3D;
        AddChild(_marker);
        _markerPosition = new Vector2I(0, 0);
    }

    private void UpdateMarker() {
        _marker.Position = new Vector3(_markerPosition.X, 0, _markerPosition.Y);
    }

    public void Save() {
        Godot.Collections.Array tileSaveData = new Godot.Collections.Array();
        Godot.Collections.Dictionary main = new Godot.Collections.Dictionary {
            {"version", "1.0.0"}
        };
        foreach(KeyValuePair<Vector2I, Tile> p in tiles) {
            Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary() {
                {"tileType", p.Value.tileType.ToString()},
                {"axialX", p.Value.position.X},
                {"axialY", p.Value.position.Y},
                {"rotationY", p.Value.Rotation.Y}
            };
            tileSaveData.Add(saveData);
        }
        main["tiles"] = tileSaveData;
        string jsonString = Json.Stringify(main, "\t");
        if (fileName == DEFAULT_FILE_NAME) {
            fileName = Guid.NewGuid().ToString("N").Substring(0, 6);
        }
        using FileAccess saveFileAccess = FileAccess.Open(SAVE_PATH + fileName + ".json", FileAccess.ModeFlags.Write);
        saveFileAccess.StoreString(jsonString);
    }

    public void LoadButton() {
        Load(fileName);
    }

    public void Load(string fileToLoad = "") {
        Clear();
        if (fileToLoad != "") {
            fileName = fileToLoad;
        }
        if (!FileAccess.FileExists(SAVE_PATH + fileName + ".json")) {
            return;
        }
        FileAccess saveFileAccess = FileAccess.Open(SAVE_PATH + fileName + ".json", FileAccess.ModeFlags.Read);
        string jsonString = saveFileAccess.GetAsText();
        Variant result = Json.ParseString(jsonString);
        if (result.VariantType == Variant.Type.Dictionary) {
            Godot.Collections.Dictionary data = (Godot.Collections.Dictionary)result;
            Godot.Collections.Array tileSaveData = (Godot.Collections.Array)data["tiles"];
            foreach (Godot.Collections.Dictionary sd in tileSaveData) {
                string tileTypeString = (string)sd["tileType"];
                TileType tileType = (TileType)Enum.Parse(typeof(TileType), tileTypeString);
                Vector2I axialCoordinate = new Vector2I((int)sd["axialX"], (int)sd["axialY"]);
                Tile tile = CreateTileAtPosition(tileType, axialCoordinate);
                tile.Rotation = new Vector3(0, (float)sd["rotationY"], 0);
                tile.Load(axialCoordinate);
            }
        }
        saveFileAccess.Close();
        _markerPosition = new Vector2I(0, 0);
        UpdateMarker();
    }

    public void Clear() {
        foreach (KeyValuePair<Vector2I, Tile> p in tiles) {
            p.Value.Free();
        }
        tiles.Clear();
    }

    public void Rotate() {
        Tile tile = GetTileAtPosition();
        if (tile != null) {
            tile.Rotate(Vector3.Up, Mathf.DegToRad(ROTATION_PER));
        }
    }

    public void MoveUp() {
        _markerPosition += new Vector2I(0, +2);
        UpdateMarker();
    }

    public void MoveDown() {
        _markerPosition += new Vector2I(0, -2);
        UpdateMarker();
    }


    public void MoveRight() {
        _markerPosition += new Vector2I(-2, 0);
        UpdateMarker();
    }

    public void MoveLeft() {
        _markerPosition += new Vector2I(+2, 0);
        UpdateMarker();
    }

    public void RemoveTile() {
        if (tiles.ContainsKey(_markerPosition)) {
            Tile tile = tiles[_markerPosition];
            tile.Free();
            tiles.Remove(_markerPosition);
        }
    }

    public void PlaceGrass() {
        CreateTileAtPosition(TileType.GRASS, _markerPosition);
    }

    public void PlaceRock() {
        CreateTileAtPosition(TileType.ROCK, _markerPosition);
    }

    public void PlaceSand() {
        CreateTileAtPosition(TileType.SAND, _markerPosition);
    }

    public void PlaceForest() {
        CreateTileAtPosition(TileType.FOREST, _markerPosition);
    }

    public void PlaceMountain() {
        CreateTileAtPosition(TileType.MOUNTAIN, _markerPosition);
    }

    public void PlaceWater() {
        CreateTileAtPosition(TileType.WATER, _markerPosition);
    }

    public void PlaceArcheryRange() {
        CreateTileAtPosition(TileType.ARCHERY_RANGE, _markerPosition);
    }

    public void PlaceBarrack() {
        CreateTileAtPosition(TileType.BARRACK, _markerPosition);
    }

    public void PlaceCastle() {
        CreateTileAtPosition(TileType.CASTLE, _markerPosition);
    }

    public void PlaceFarm() {
        CreateTileAtPosition(TileType.FARM, _markerPosition);
    }

    public void PlaceHouse() {
        CreateTileAtPosition(TileType.HOUSE, _markerPosition);
    }

    public void PlaceLumbermill() {
        CreateTileAtPosition(TileType.LUMBERMILL, _markerPosition);
    }

    public void PlaceMarket() {
        CreateTileAtPosition(TileType.MARKET, _markerPosition);
    }

    public void PlaceMill() {
        CreateTileAtPosition(TileType.MILL, _markerPosition);
    }

    public void PlaceMine() {
        CreateTileAtPosition(TileType.MINE, _markerPosition);
    }

    public void PlaceTower() {
        CreateTileAtPosition(TileType.TOWER, _markerPosition);
    }

    public void PlaceWaterMill() {
        CreateTileAtPosition(TileType.WATER_MILL, _markerPosition);
    }

    public void PlaceWell() {
        CreateTileAtPosition(TileType.WELL, _markerPosition);
    }

    public Tile GetTileAtPosition() {
        return tiles.ContainsKey(_markerPosition) ? tiles[_markerPosition] : null;
    }

    private Tile CreateTileAtPosition(TileType tileType, Vector2I position) {
        if (tiles.ContainsKey(position)) {
            RemoveTile();
        }
        PackedScene prefab = GetPrefabByType(tileType);
        Node n = prefab.Instantiate();
        Tile tile = n as Tile;
        AddChild(n);
        tile.tileType = tileType;
        tile.Load(position);
        tiles[position] = tile;
        return tile;
    }

    private PackedScene GetPrefabByType(TileType tileType) {
        return tileType switch
        {
            TileType.GRASS => grassTilePrefab,
            TileType.ROCK => rockTilePrefab,
            TileType.SAND => sandTilePrefab,
            TileType.FOREST => forestTilePrefab,
            TileType.MOUNTAIN => mountainTilePrefab,
            TileType.WATER => waterTilePrefab,
            TileType.ARCHERY_RANGE => archeryRangeTilePrefab,
            TileType.BARRACK => barrackTilePrefab,
            TileType.CASTLE => castleTilePrefab,
            TileType.FARM => farmTilePrefab,
            TileType.HOUSE => houseTilePrefab,
            TileType.LUMBERMILL => lumbermillTilePrefab,
            TileType.MARKET => marketTilePrefab,
            TileType.MILL => millTilePrefab,
            TileType.MINE => mineTilePrefab,
            TileType.TOWER => towerTilePrefab,
            TileType.WATER_MILL => waterMillTilePrefab,
            TileType.WELL => wellTilePrefab,
            _ => grassTilePrefab,
        };

    }
}
