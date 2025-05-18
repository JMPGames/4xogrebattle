using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[Tool]
public partial class BoardCreator : Node {
    private const string SAVE_PATH = "res://data/levels/";
    private const string TILE_PREFAB_PATH = "res://prefabs/tiles/";
    private const string TROOP_SPAWN_PREFAB_PATH = "res://prefabs/boardCreator/";
    private const float ROTATION_PER = 90.0f;
    private const string DEFAULT_FILE_NAME = "default";

    [Export] private int _goldReward = 100;
    [Export] private int _width = 10;
    [Export] private int _height = 10;
    [Export] private int _lakeMinSize = 3;
    [Export] private int _lakeMaxSize = 5;
    [Export] private double _chanceForLake = 0.5f;
    [Export] public string fileName = DEFAULT_FILE_NAME;

    public List<Troop> EnemyTroops {get; private set;}
    public Godot.Collections.Dictionary BoardRewards {get; private set;}

    private Node3D _marker;
    private Vector2I _markerPosition;
    private Dictionary<Vector2I, Tile> _tiles = new Dictionary<Vector2I, Tile>();
    private Dictionary<Faction, List<Node3D>> _spawnPoints = new Dictionary<Faction, List<Node3D>>();

    private PackedScene tileSelectorPrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "TileSelector.tscn");
    private PackedScene playerTroopSpawnPrefab = GD.Load<PackedScene>(TROOP_SPAWN_PREFAB_PATH + "PlayerTroopSpawn.tscn");
    private PackedScene enemyTroopSpawnPrefab = GD.Load<PackedScene>(TROOP_SPAWN_PREFAB_PATH + "EnemyTroopSpawn.tscn");

    public override void _Ready() {
        SetProcessInput(true);
        EnemyTroops = new List<Troop>();
        _spawnPoints[Faction.PLAYER] = new List<Node3D>();
        _spawnPoints[Faction.ENEMY] = new List<Node3D>();
        _marker = tileSelectorPrefab.Instantiate() as Node3D;
        AddChild(_marker);
        _markerPosition = new Vector2I(0, 0);
    }

    private void UpdateMarker() {
        _marker.Position = new Vector3(_markerPosition.X, 0, _markerPosition.Y);
    }

    public void Save() {
        Godot.Collections.Array tileSaveData = new Godot.Collections.Array();
        Godot.Collections.Array enemySpawnSaveData = new Godot.Collections.Array();
        Godot.Collections.Array playerSpawnSaveData = new Godot.Collections.Array();
        Godot.Collections.Dictionary rewards = new Godot.Collections.Dictionary {
            {"gold", _goldReward}
        };
        Godot.Collections.Dictionary main = new Godot.Collections.Dictionary {
            {"version", "1.0.0"},
            {"rewards", rewards}
        };

        List<Node3D> playerSpawns = _spawnPoints[Faction.PLAYER];
        for (int i = 0; i < playerSpawns.Count; i++) {
            Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary() {
                {"positionX", playerSpawns[i].Position.X},
                {"positionY", playerSpawns[i].Position.Z}
            };
            playerSpawnSaveData.Add(saveData);
        }
        main["playerSpawns"] = playerSpawnSaveData;

        List<Node3D> enemySpawns = _spawnPoints[Faction.ENEMY];
        for (int i = 0; i < enemySpawns.Count; i++) {
            Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary() {
                {"positionX", enemySpawns[i].Position.X},
                {"positionY", enemySpawns[i].Position.Z}
            };
            enemySpawnSaveData.Add(saveData);
        }
        main["enemySpawns"] = enemySpawnSaveData;

        foreach(KeyValuePair<Vector2I, Tile> p in _tiles) {
            Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary() {
                {"tileType", p.Value.TileType.ToString()},
                {"positionX", p.Value.Pos.X},
                {"positionY", p.Value.Pos.Y},
                {"rotationY", p.Value.Rotation.Y}
            };
            tileSaveData.Add(saveData);
        }
        if (tileSaveData.Count <= 0) {
            return;
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
        Load(fileName, false);
    }

    public void Load(string fileToLoad = "", bool runtime = true) {
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
            Godot.Collections.Array enemySpawns = (Godot.Collections.Array)data["enemySpawns"];
            Godot.Collections.Array playerSpawns = (Godot.Collections.Array)data["playerSpawns"];
            BoardRewards = (Godot.Collections.Dictionary)data["rewards"];
            foreach (Godot.Collections.Dictionary sd in tileSaveData) {
                string tileTypeString = (string)sd["tileType"];
                TileType tileType = Common.GetEnumFromString<TileType>(tileTypeString);
                Vector2I position = new Vector2I((int)sd["positionX"], (int)sd["positionY"]);
                Tile tile = CreateTileAtPosition(tileType, position);
                tile.Rotation = new Vector3(0, (float)sd["rotationY"], 0);
                tile.Load(position, tileType);

                List<Variant> filteredPlayerSpawns = GetSpawnPointsAtPosition(playerSpawns, position);
                if (!runtime && filteredPlayerSpawns.Count > 0) {
                    CreateTroopSpawnAtPosition(position, Faction.PLAYER);
                }

                List<Variant> filteredEnemySpawns = GetSpawnPointsAtPosition(enemySpawns, position);
                if (filteredEnemySpawns.Count > 0) {
                    Godot.Collections.Dictionary enemyTroopData = (Godot.Collections.Dictionary)filteredEnemySpawns[0];
                    if (runtime) {
                        // CreateTroopAtPosition(tile, (Godot.Collections.Array)enemyTroopData["formation"]);
                    }
                    else {
                        CreateTroopSpawnAtPosition(position, Faction.ENEMY);
                    }
                }
            }
        }
        saveFileAccess.Close();
        _markerPosition = runtime ? new Vector2I(-200, -200) : new Vector2I(0, 0);
        UpdateMarker();
    }

    private List<Variant> GetSpawnPointsAtPosition(Godot.Collections.Array spawns, Vector2I position) {
        return spawns.Where(
            s => {
                if (s.VariantType != Variant.Type.Dictionary) {
                    return false;
                }
                Godot.Collections.Dictionary data = (Godot.Collections.Dictionary)s;
                if (!data.ContainsKey("positionX") || !data.ContainsKey("positionY")) {
                    return false;
                }
                return (int)data["positionX"] == position.X && (int)data["positionY"] == position.Y;
            }
        ).ToList();
    }

    public void GenerateMap() {
        Random rand = new Random();
        TileType[] weightTerrainTypes = {
            TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.GRASS,
            TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.GRASS,
            TileType.FOREST, TileType.FOREST, TileType.FOREST, TileType.FOREST,
            TileType.MOUNTAIN,
            TileType.ROCK,
        };
        int lakeWidth = rand.Next(_lakeMinSize, Math.Min(_lakeMaxSize + 1, _width - 2));
        int lakeHeight = rand.Next(_lakeMinSize, Math.Min(_lakeMaxSize + 1, _height - 2));
        int lakeStartX = rand.Next(1, _width - lakeWidth - 1);
        int lakeStartY = rand.Next(1, _height - lakeHeight - 1);
        bool hasLake = rand.NextDouble() <= _chanceForLake;
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                Vector2I position = new Vector2I(x * Common.TILE_SIZE, y * Common.TILE_SIZE);
                if (
                    hasLake &&
                    x >= lakeStartX && x < lakeStartX + lakeWidth &&
                    y >= lakeStartY && y < lakeStartY + lakeHeight
                ) {
                    CreateTileAtPosition(TileType.WATER, position);
                }
                else {
                    TileType tt = weightTerrainTypes[rand.Next(weightTerrainTypes.Length)];
                    CreateTileAtPosition(tt, position);
                }
            }
        }
        _markerPosition = new Vector2I(0, 0);
        UpdateMarker();
    }

    public Tile GetTileAtPosition() {
        return _tiles.ContainsKey(_markerPosition) ? _tiles[_markerPosition] : null;
    }

    private Tile CreateTileAtPosition(TileType tileType, Vector2I position) {
        if (_tiles.ContainsKey(position)) {
            RemoveTile();
        }
        PackedScene prefab = Common.GetTilePrefabByType(tileType);
        Node tileNode = prefab.Instantiate();
        Tile tile = tileNode as Tile;
        AddChild(tileNode);
        tile.Load(position, tileType);
        _tiles[position] = tile;
        return tile;
    }

    private void CreateTroopSpawnAtPosition(Vector2I position, Faction faction) {
        PackedScene prefab = faction == Faction.PLAYER ? playerTroopSpawnPrefab : enemyTroopSpawnPrefab;
        Node3D spawnNode = prefab.Instantiate() as Node3D;
        AddChild(spawnNode);
        spawnNode.Position = new Vector3(position.X, 1, position.Y);
        _spawnPoints[faction].Add(spawnNode);
    }

    private Troop CreateTroopAtPosition(Tile tile, Godot.Collections.Array formation) {
        Node troopNode = Common.GetTroopPrefab.Instantiate();
        Troop troop = troopNode as Troop;
        AddChild(troopNode);
        BattleEntity[] battleEntities = new BattleEntity[Common.MAX_ENTITIES_PER_TROOP];
        for (int i = 0; i < formation.Count; i++) {
            if (formation[i].VariantType != Variant.Type.Dictionary) {
                continue;
            }
            else {
                Godot.Collections.Dictionary battleEntityData = (Godot.Collections.Dictionary)formation[i];
                string battleJobString = (string)battleEntityData["type"];
                BattleJob battleJob = Common.GetEnumFromString<BattleJob>(battleJobString);
                Node3D battleEntityNode = (Node3D)Common.GetJobPrefabByType(battleJob).Instantiate();
                BattleEntity battleEntity = battleEntityNode as BattleEntity;
                AddChild(battleEntityNode);
                battleEntityNode.Visible = false;
                battleEntity.Load(troop);
                battleEntities[i] = battleEntity;
                troop.Formation.SetBattleEntity(i, battleEntity);
            }
        }
        troop.Load(tile, Facing.NORTH, battleEntities);
        EnemyTroops.Add(troop);
        return troop;
    }

    #region Plugin Button Functions
        public void Clear() {
        foreach (KeyValuePair<Vector2I, Tile> p in _tiles) {
            p.Value.Free();
        }
        _tiles.Clear();

        if (_spawnPoints.ContainsKey(Faction.PLAYER)) {
            List<Node3D> playerSpawns = _spawnPoints[Faction.PLAYER];
            for (int i = 0; i < playerSpawns.Count; i++) {
                playerSpawns[i].Free();
            }
            _spawnPoints[Faction.PLAYER].Clear();
        }

        if (_spawnPoints.ContainsKey(Faction.ENEMY)) {
            List<Node3D> enemySpawns = _spawnPoints[Faction.ENEMY];
            for (int i = 0; i < enemySpawns.Count; i++) {
                enemySpawns[i].Free();
            }
            _spawnPoints[Faction.ENEMY].Clear();
        }
    }

    public void Rotate() {
        Tile tile = GetTileAtPosition();
        if (tile != null) {
            tile.Rotate(Vector3.Up, Mathf.DegToRad(ROTATION_PER));
        }
    }

    public void MoveUp() {
        _markerPosition += new Vector2I(0, +Common.TILE_SIZE);
        UpdateMarker();
    }

    public void MoveDown() {
        _markerPosition += new Vector2I(0, -Common.TILE_SIZE);
        UpdateMarker();
    }

    public void MoveRight() {
        _markerPosition += new Vector2I(-Common.TILE_SIZE, 0);
        UpdateMarker();
    }

    public void MoveLeft() {
        _markerPosition += new Vector2I(+Common.TILE_SIZE, 0);
        UpdateMarker();
    }

    public void RemoveTile() {
        if (_tiles.ContainsKey(_markerPosition)) {
            Tile tile = _tiles[_markerPosition];
            tile.Free();
            _tiles.Remove(_markerPosition);
        }
    }

    public void PlacePlayerTroopSpawn() {
        CreateTroopSpawnAtPosition(_markerPosition, Faction.PLAYER);
    }

    public void PlaceEnemyTroopSpawn() {
        CreateTroopSpawnAtPosition(_markerPosition, Faction.ENEMY);
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
    #endregion
}
