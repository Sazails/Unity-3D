using UnityEngine;

namespace Assets._Core.Scripts._Utilities
{
    public static class SazInput
    {
        public static KeyCode _moveUp = KeyCode.W; // W or Up arrow
        public static KeyCode _moveDown = KeyCode.S; // S or Down arrow
        public static KeyCode _moveLeft = KeyCode.A;  // A or Left arrow
        public static KeyCode _moveRight = KeyCode.D;  // D or Right arrow
        public static KeyCode _jump = KeyCode.Space;  // Space
        public static KeyCode _crouch;  // Left Ctrl
        public static KeyCode _primaryAttack; // Shoots
        public static KeyCode _secondaryAttack; // Aim

        public static int GetMoveHorizontalRaw()
        {
            if (Input.GetKey(_moveUp))
                return 1;
            else if (Input.GetKey(_moveDown))
                return -1;
            else
                return 0;
        }

        public static int GetMoveVerticalRaw()
        {
            if (Input.GetKey(_moveRight))
                return 1;
            else if (Input.GetKey(_moveLeft))
                return -1;
            else
                return 0;
        }
    }
}