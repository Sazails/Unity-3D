using UnityEngine;

namespace _Project.Scripts._Utilities
{
    public static class CursorUtility
    {
        public static void LockAndHideCursor(bool state)
        {
            Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !state;
        }
        
        public static void LockCursor(bool state)
        {
            Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public static void HideCursor(bool state)
        {
            Cursor.visible = !state;
        }
    }
}