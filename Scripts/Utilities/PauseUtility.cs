using UnityEngine;

namespace _Project.Scripts._Utilities
{
    public static class PauseUtility
    {
        private static bool _isPaused = false;
        
        public static bool IsPaused()
        {
            return _isPaused;
        }
        
        public static void Pause(bool state)
        {
            _isPaused = state;
            Toggle();
        }

        public static void TogglePause()
        {
            _isPaused = !_isPaused;
            Toggle();
        }

        private static void Toggle()
        {
            AudioListener.pause = _isPaused;
            Time.timeScale = _isPaused ? 0F : 1F;
            Debug.Log("Paused: " + _isPaused);
        }
    }
}