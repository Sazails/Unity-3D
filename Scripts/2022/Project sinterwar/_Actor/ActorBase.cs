using UnityEngine;

namespace Assets._Core.Scripts._Actor
{
    /// <summary>
    /// Base class of the Actor, ties all components needed together, gets data needed from support classes. Example: Get decision from ActorBrain for new position, and pass the input to the ActorController.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class ActorBase : MonoBehaviour
    {
        /// <summary>
        /// Actor ID is used to identify the actor as unique in the scene from other Actors.
        /// Id meanings:
        /// -1 = Not assigned to an identifier. Usually used as a check if it needs an identifier.
        /// 0 to MAX = Unique Actor identifier. Usually assigned by the game controller.
        /// </summary>
        public int _actorId = -1;

        public ActorBrain _brain = new ActorBrain();
        public ActorController _controller = new ActorController();
        public ActorHealth _actor = new ActorHealth();

        public void Initialize(bool isPlayerControlled, int actorId, CharacterController charController)
        {
            _actorId = actorId;
            _brain.Initialize(isPlayerControlled);
            _controller.Initialize(charController, transform);
        }

        private void Update()
        {
            _controller.UpdateController(_brain.GetInput());
        }
    }
}