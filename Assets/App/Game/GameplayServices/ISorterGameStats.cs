namespace App.Game
{
    public interface ISorterGameStats
    {
        int FiguresRemainsToWin { get; }
        int PlayerScore { get; }
        int PlayerHealth { get; }
    }
}