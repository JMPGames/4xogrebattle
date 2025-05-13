using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class BoardCreator : Node {
    private const string SAVE_PATH = "res://data/levels/";
    private const string TILE_PREFAB_PATH = "res://prefabs/tiles/";
    private const float ROTATION_PER = 60.0f;
    private const string DEFAULT_FILE_NAME = "default";

    [Export] public string fileName = DEFAULT_FILE_NAME;

    private Node3D _marker;
    private Vector2I _axialCoordinate;
    private Dictionary<Vector2I, Tile> tiles = new Dictionary<Vector2I, Tile>();

    private PackedScene tileSelectorPrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "TileSelector.tscn");
    private PackedScene grassTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "GrassTile.tscn");
    private PackedScene treeTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "TreeTile.tscn");
    private PackedScene hillTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "HillTile.tscn");
    private PackedScene mountainTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "MountainTile.tscn");
    private PackedScene waterTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "WaterTile.tscn");
    private PackedScene farmTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "FarmTile.tscn");
    private PackedScene houseTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "HouseTile.tscn");
    private PackedScene castleTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "CastleTile.tscn");
    private PackedScene dockTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "DockTile.tscn");
    private PackedScene mineTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "MineTile.tscn");
    private PackedScene animalTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "AnimalTile.tscn");
    private PackedScene blacksmithTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "BlacksmithTile.tscn");
    private PackedScene towerTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "TowerTile.tscn");
    private PackedScene sandTilePrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "SandTile.tscn");

    public override void _Ready() {
        SetProcessInput(true);
        _marker = tileSelectorPrefab.Instantiate() as Node3D;
        AddChild(_marker);
        _axialCoordinate = new Vector2I(0, 0);
    }

    private void UpdateMarker() {
        _marker.Position = Common.GetHexPositionFromAxial(_axialCoordinate);
    }

    public void Save() {
        Godot.Collections.Array tileSaveData = new Godot.Collections.Array();
        Godot.Collections.Dictionary main = new Godot.Collections.Dictionary {
            {"version", "1.0.0"}
        };
        foreach(KeyValuePair<Vector2I, Tile> p in tiles) {
            Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary() {
                {"tileType", p.Value.tileType.ToString()},
                {"axialX", p.Value.axialCoordinate.X},
                {"axialY", p.Value.axialCoordinate.Y},
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

    public void Load() {
        Clear();
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
                Tile tile = CreateTileAtAxialPosition(tileType, axialCoordinate);
                tile.Rotation = new Vector3(0, (float)sd["rotationY"], 0);
                tile.Load(axialCoordinate);
            }
        }
        saveFileAccess.Close();
        _axialCoordinate = new Vector2I(0, 0);
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

    public void MoveUpLeft() {
        _axialCoordinate += new Vector2I(0, +1);
        UpdateMarker();
    }

    public void MoveLeft() {
        _axialCoordinate += new Vector2I(+1, 0);
        UpdateMarker();
    }

    public void MoveDownLeft() {
        _axialCoordinate += new Vector2I(+1, -1);
        UpdateMarker();
    }

    public void MoveUpRight() {
        _axialCoordinate += new Vector2I(-1, +1);
        UpdateMarker();
    }

    public void MoveRight() {
        _axialCoordinate += new Vector2I(-1, 0);
        UpdateMarker();
    }

    public void MoveDownRight() {
        _axialCoordinate += new Vector2I(0, -1);
        UpdateMarker();
    }

    public void RemoveTile() {
        if (tiles.ContainsKey(_axialCoordinate)) {
            Tile tile = tiles[_axialCoordinate];
            tile.Free();
            tiles.Remove(_axialCoordinate);
        }
    }

    public void PlaceGrass() {
        CreateTileAtAxialPosition(TileType.GRASS, _axialCoordinate);
    }

    public void PlaceTrees() {
        CreateTileAtAxialPosition(TileType.TREES, _axialCoordinate);
    }

    public void PlaceHill() {
        CreateTileAtAxialPosition(TileType.HILL, _axialCoordinate);
    }

    public void PlaceMountain() {
        CreateTileAtAxialPosition(TileType.MOUNTAIN, _axialCoordinate);
    }

    public void PlaceWater() {
        CreateTileAtAxialPosition(TileType.WATER, _axialCoordinate);
    }

    public void PlaceFarm() {
        CreateTileAtAxialPosition(TileType.FARM, _axialCoordinate);
    }

    public void PlaceHouse() {
        CreateTileAtAxialPosition(TileType.HOUSE, _axialCoordinate);
    }

    public void PlaceCastle() {
        CreateTileAtAxialPosition(TileType.CASTLE, _axialCoordinate);
    }

    public void PlaceDock() {
        CreateTileAtAxialPosition(TileType.DOCK, _axialCoordinate);
    }

    public void PlaceMine() {
        CreateTileAtAxialPosition(TileType.MINE, _axialCoordinate);
    }

    public void PlaceAnimals() {
        CreateTileAtAxialPosition(TileType.ANIMALS, _axialCoordinate);
    }

    public void PlaceBlacksmith() {
        CreateTileAtAxialPosition(TileType.BLACKSMITH, _axialCoordinate);
    }

    public void PlaceTower() {
        CreateTileAtAxialPosition(TileType.TOWER, _axialCoordinate);
    }

    public void PlaceSand() {
        CreateTileAtAxialPosition(TileType.SAND, _axialCoordinate);
    }

    private Tile GetTileAtPosition() {
        return tiles.ContainsKey(_axialCoordinate) ? tiles[_axialCoordinate] : null;
    }

    private Tile CreateTileAtAxialPosition(TileType tileType, Vector2I axialCoordinate) {
        if (tiles.ContainsKey(axialCoordinate)) {
            RemoveTile();
        }
        PackedScene prefab = GetPrefabByType(tileType);
        Node n = prefab.Instantiate();
        Tile tile = n as Tile;
        AddChild(n);
        tile.tileType = tileType;
        tile.Load(axialCoordinate);
        tiles[axialCoordinate] = tile;
        GD.Print(tiles.Count);
        return tile;
    }

    private PackedScene GetPrefabByType(TileType tileType) {
        return tileType switch
        {
            TileType.GRASS => grassTilePrefab,
            TileType.TREES => treeTilePrefab,
            TileType.HILL => hillTilePrefab,
            TileType.MOUNTAIN => mountainTilePrefab,
            TileType.WATER => waterTilePrefab,
            TileType.FARM => farmTilePrefab,
            TileType.HOUSE => houseTilePrefab,
            TileType.CASTLE => castleTilePrefab,
            TileType.DOCK => dockTilePrefab,
            TileType.MINE => mineTilePrefab,
            TileType.ANIMALS => animalTilePrefab,
            TileType.BLACKSMITH => blacksmithTilePrefab,
            TileType.TOWER => towerTilePrefab,
            TileType.SAND => sandTilePrefab,
            _ => grassTilePrefab,
        };

    }
}
