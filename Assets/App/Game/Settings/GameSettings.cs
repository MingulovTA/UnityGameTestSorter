using App.Game.Settings.CustomTypes;

namespace App.Game.Settings
{
    public class GameSettings
    {
        public RangeInt FiguresCountToWin = new RangeInt(10,20);
        public RangeFloat DelayBeforeSpawnFigure = new RangeFloat(1.5f,2.9f);
        public RangeFloat FiguresSpeed = new RangeFloat(0.05f,0.2f);
        public int PlayerHealth = 10;
    }
}
