using System;
using Godot;

public static class Common {
    public const string BOARD_LEVEL_PATH = "res://data/levels/";
    public const string TILE_PREFAB_PATH = "res://prefabs/tiles/";
    public const string BATTLE_ENTITY_PREFAB_PATH = "res://prefabs/battleEntities/";
    public const int MAX_ENTITIES_PER_TROOP = 6;
    public const int TILE_SIZE = 2;

    public static PackedScene GetTroopPrefab => GD.Load<PackedScene>("res://prefabs/Troop.tscn");

    public static bool IndexAndValueExistsInArray<T>(int i, T[] a) {
        return IndexExistsInArray(i, a) && a[i] != null;
    }

    public static bool IndexExistsInArray<T>(int i, T[] a) {
        return i >= 0 && i < a.Length;
    }

    public static T GetEnumFromString<T>(string s) {
        return (T)Enum.Parse(typeof(T), s);
    }

    public static PackedScene GetTilePrefabByType(TileType tileType) {
        PackedScene grassPrefab = GD.Load<PackedScene>(TILE_PREFAB_PATH + "GrassTile.tscn");
        return tileType switch
        {
            TileType.GRASS => grassPrefab,
            TileType.ROCK => GD.Load<PackedScene>(TILE_PREFAB_PATH + "RockTile.tscn"),
            TileType.SAND => GD.Load<PackedScene>(TILE_PREFAB_PATH + "SandTile.tscn"),
            TileType.FOREST => GD.Load<PackedScene>(TILE_PREFAB_PATH + "ForestTile.tscn"),
            TileType.MOUNTAIN => GD.Load<PackedScene>(TILE_PREFAB_PATH + "MountainTile.tscn"),
            TileType.WATER => GD.Load<PackedScene>(TILE_PREFAB_PATH + "WaterTile.tscn"),
            TileType.ARCHERY_RANGE => GD.Load<PackedScene>(TILE_PREFAB_PATH + "ArcheryRangeTile.tscn"),
            TileType.BARRACK => GD.Load<PackedScene>(TILE_PREFAB_PATH + "BarrackTile.tscn"),
            TileType.CASTLE => GD.Load<PackedScene>(TILE_PREFAB_PATH + "CastleTile.tscn"),
            TileType.FARM => GD.Load<PackedScene>(TILE_PREFAB_PATH + "FarmTile.tscn"),
            TileType.HOUSE => GD.Load<PackedScene>(TILE_PREFAB_PATH + "HouseTile.tscn"),
            TileType.LUMBERMILL => GD.Load<PackedScene>(TILE_PREFAB_PATH + "LumbermillTile.tscn"),
            TileType.MARKET => GD.Load<PackedScene>(TILE_PREFAB_PATH + "MarketTile.tscn"),
            TileType.MILL => GD.Load<PackedScene>(TILE_PREFAB_PATH + "MillTile.tscn"),
            TileType.MINE => GD.Load<PackedScene>(TILE_PREFAB_PATH + "MineTile.tscn"),
            TileType.TOWER => GD.Load<PackedScene>(TILE_PREFAB_PATH + "TowerTile.tscn"),
            TileType.WATER_MILL => GD.Load<PackedScene>(TILE_PREFAB_PATH + "WaterMillTile.tscn"),
            TileType.WELL => GD.Load<PackedScene>(TILE_PREFAB_PATH + "WellTile.tscn"),
            _ => grassPrefab,
        };
    }

    public static PackedScene GetJobPrefabByType(BattleJob battleJob) {
        return battleJob switch {
            BattleJob.FIGHTER => GD.Load<PackedScene>(BATTLE_ENTITY_PREFAB_PATH + "Fighter.tscn"),
            BattleJob.KNIGHT => GD.Load<PackedScene>(BATTLE_ENTITY_PREFAB_PATH + "Knight.tscn"),
            BattleJob.ROGUE => GD.Load<PackedScene>(BATTLE_ENTITY_PREFAB_PATH + "Rogue.tscn"),
            BattleJob.ARCHER => GD.Load<PackedScene>(BATTLE_ENTITY_PREFAB_PATH + "Archer.tscn"),
            BattleJob.CLERIC => GD.Load<PackedScene>(BATTLE_ENTITY_PREFAB_PATH + "Cleric.tscn"),
            BattleJob.MAGE => GD.Load<PackedScene>(BATTLE_ENTITY_PREFAB_PATH + "Mage.tscn"),
            BattleJob.ORC => GD.Load<PackedScene>(BATTLE_ENTITY_PREFAB_PATH + "Orc.tscn"),
            BattleJob.GOBLIN => GD.Load<PackedScene>(BATTLE_ENTITY_PREFAB_PATH + "Goblin.tscn"),
            BattleJob.DEMON_MAGE => GD.Load<PackedScene>(BATTLE_ENTITY_PREFAB_PATH + "DemonMage.tscn"),
            _ => null
        };
    }
}