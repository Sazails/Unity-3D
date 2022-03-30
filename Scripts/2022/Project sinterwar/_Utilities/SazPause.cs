namespace Assets._Core.Scripts._Utilities
{
    public static class SazPause
    {
        public static bool _isGamePaused { get; private set; }

        public static void SetPaused(bool state)
        {
            _isGamePaused = state;
        }
    }
}