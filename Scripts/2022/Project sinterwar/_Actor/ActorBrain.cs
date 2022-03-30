using Assets._Core.Scripts._Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._Core.Scripts._Actor
{
    /// <summary>
    /// Get all the decisions for pathfinding, shooting, movement of the Actor from this class. ActorBase will call this class the most, it will get the data to pass it on to other classes.
    /// </summary>
    public class ActorBrain
    {
        public bool _isPlayerControlled = false;

        public void Initialize(bool isPayerControlled)
        {
            _isPlayerControlled = isPayerControlled;
        }

        public Vector2Int GetInput()
        {
            if(_isPlayerControlled)
            {
                return new Vector2Int(SazInput.GetMoveHorizontalRaw(), SazInput.GetMoveVerticalRaw());
            }

            // Bot controlled
            return new Vector2Int(0, 0);
        }
    }
}