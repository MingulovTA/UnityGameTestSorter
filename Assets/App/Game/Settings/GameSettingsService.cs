using App.Game.Settings;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

public class GameSettingsService: IInitializable, IGameSettingsService
{
    private const string GAME_SETTINGS_PREFS_KEY = "com.game.settings";
    private GameSettings _gameSettings;

    public GameSettings GameSettings => _gameSettings;

    public void Initialize() => Load();

    public void Save()
    {
        string json = JsonConvert.SerializeObject(_gameSettings);
        PlayerPrefs.SetString(GAME_SETTINGS_PREFS_KEY,json);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(GAME_SETTINGS_PREFS_KEY))
        {
            LoadFromPlayerPrefs();
        }
        else
        {
            CreateNewSettings();
            Save();
        }
    }

    private void LoadFromPlayerPrefs()
    {
        string json = PlayerPrefs.GetString(GAME_SETTINGS_PREFS_KEY);
        _gameSettings = JsonConvert.DeserializeObject<GameSettings>(json);
    }

    private void CreateNewSettings() => _gameSettings = new GameSettings();
}
