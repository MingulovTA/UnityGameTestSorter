using App.Game.Settings.CustomTypes;

namespace App.Game.Settings
{
    public class GameSettings
    {
        public RangeInt FiguresCountToWin = new RangeInt(10,15);
        public RangeFloat DelayBeforeSpawnFigure = new RangeFloat(1.5f,2.0f);
        public RangeFloat FiguresSpeed = new RangeFloat(0.1f,0.25f);
        public int PlayerHealth = 10;
    }
}
