using App.Game.Settings;

public interface IGameSettingsService
{
    GameSettings GameSettings { get; }
    void Save();
}