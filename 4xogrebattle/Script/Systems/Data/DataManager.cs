using Godot;
using System;
using System.IO;
using System.Text.Json;

public partial class DataManager : Node {
    public static DataManager Instance {get; private set;}

    private readonly string SAVE_PATH = Path.Combine(OS.GetUserDataDir(), "save.json");
    public GameData CurrentData {get; private set;} = new GameData();

    public void SaveGameData() {
        try {
            string json = JsonSerializer.Serialize(CurrentData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SAVE_PATH, json);
        }
        catch (Exception e) {
            GD.PrintErr($"Error saving game: {e.Message}");
        }
    }

    public void LoadGameData() {
        try {
            if (File.Exists(SAVE_PATH)) {
                string json = File.ReadAllText(SAVE_PATH);
                CurrentData = JsonSerializer.Deserialize<GameData>(json) ?? new GameData();
            }
            else {
                CurrentData = new GameData();
            }
        }
        catch (Exception e) {
            GD.PrintErr($"Error loading game: {e.Message}");
        }
    }
}