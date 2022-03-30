using UnityEngine;

namespace Assets._Core.Scripts._Actor
{
    /// <summary>
    /// Class is only responsible for the movement of the physical Actor, all decisions are made by the ActorBrain and passed to this class as input.
    /// </summary>
    public class ActorController
    {
        public enum MoveState
        {
            Idle,
            Move,
            Airborn,
        }

        private CharacterController _controller;
        public MoveState _moveState = MoveState.Idle;

        public Transform _controllerTransform;
        public Vector3 _moveDirection;
        public float _yVelocity;

        public int _currentMoveSpeed = 5;
        private const int _normalSpeed = 5;
        private const int _airSpeed = 4;
        public float _speedMultiplier = 1F;
        private float _gravity = -9.81F;

        public bool _isGrounded;
        public bool _isDebug = false;

        private const float _controllerScaleRayOffsetY = 0.2F;

        public void Initialize(CharacterController controller, Transform controllerTrans)
        {
            _controller = controller;
            _gravity = Physics.gravity.y * 3;
        }

        public void UpdateController(Vector2Int moveInputDir)
        {
            _isGrounded = _controller.isGrounded;

            // Input 
            _moveDirection = Vector3.Normalize(new Vector3(moveInputDir.x, 0, moveInputDir.y));
            _moveDirection = _controllerTransform.TransformDirection(_moveDirection);

            ChangeMoveSpeed();

            _moveDirection *= _currentMoveSpeed * _speedMultiplier;

            if (_isGrounded)
            {
                _yVelocity = -1;
                /*if (Input.GetKeyDown(SazInput.sazKeyInputData.KeyJump) && !GetHitAbove(normalControllerScaleY + controllerScaleRayOffsetY))
                {
                    yVelocity = jumpSpeed;
                    Crouch(false);
                }*/
            }
            else
            {
                // If surface above head is hit from jumping, reset controller velocity to avoid floating.
                if (GetHitAbove(_controllerTransform.localScale.y + _controllerScaleRayOffsetY))
                    _yVelocity += _gravity * Time.deltaTime;
            }

            _yVelocity += _gravity * Time.deltaTime;
            _moveDirection.y = _yVelocity;
            _controller.Move(_moveDirection * Time.deltaTime);

            Debug.DrawRay(_controllerTransform.position, _controllerTransform.up * (_controllerTransform.localScale.y + 0.2F));
        }

        private void ChangeMoveSpeed()
        {
            if (!_isGrounded)
            {
                _currentMoveSpeed = _airSpeed;
                _moveState = MoveState.Airborn;
            }
            else if (_moveDirection == Vector3.zero)
            {
                _moveState = MoveState.Idle;
            }
            else
            {
                _currentMoveSpeed = _normalSpeed;
                _moveState = MoveState.Move;
            }
        }

        private bool GetHitAbove(float distance)
        {
            return Physics.Raycast(_controllerTransform.position, Vector3.up, distance);
        }
    }
}