using App.Game.Settings.CustomTypes;

namespace App.Game.Settings
{
    public class GameSettings
    {
        public RangeInt FiguresCountToWin = new RangeInt(10,20);
        public RangeFloat DelayBeforeSpawnFigure = new RangeFloat(.5f,.9f);
        public RangeFloat FiguresSpeed = new RangeFloat(2.1f,1.2f);
        public int PlayerHealth = 10;
    }
}
