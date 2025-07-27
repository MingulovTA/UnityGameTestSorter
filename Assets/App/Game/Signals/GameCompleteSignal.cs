namespace App.Game.Signals
{
    public class GameCompleteSignal
    {
        private readonly bool _isPlayerWin;

        public bool IsPlayerWin => _isPlayerWin;

        public GameCompleteSignal(bool isPlayerWin)
        {
            _isPlayerWin = isPlayerWin;
        }
    }
}
