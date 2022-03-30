using Assets._Game.Scripts._Core;
using UnityEngine;

namespace Assets._Game.Scripts._Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private PlayerMoveState moveState;

        public Vector3 moveDirection;
        public float yVelocity;

        public int currentMoveSpeed = 5;
        private const int normalSpeed = 5;
        private const int crouchSpeed = 3;
        private const int airSpeed = 4;
        private const int jumpSpeed = 7;
        public float speedMultiplier = 1F;
        private float gravity = -9.81F;

        public bool crouching;
        public bool grounded;
        public bool debug = false;

        private const float controllerScaleRayOffsetY = 0.2F;
        private const float normalControllerScaleY = 0.85F;
        private const float crouchControllerScaleY = 0.6F;

        private void Start()
        {
            if (!controller)
                GetComponent<CharacterController>();

            gravity = Physics.gravity.y * 3;
        }

        private void Update()
        {
            if (SazGlobalPause.Paused)
                return;

            MoveController();
        }
        
        private void GetInput()
        {
            moveDirection = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetKeyDown(SazInput.sazKeyInputData.KeyCrouch))
                Crouch(!crouching);
        }

        private void ChangeMoveSpeed()
        {
            if (!grounded)
            {
                currentMoveSpeed = airSpeed;
                moveState.SetState(PlayerMoveState.MoveState.Airborn);
            }
            else if (crouching)
            {
                currentMoveSpeed = crouchSpeed;
                moveState.SetState(PlayerMoveState.MoveState.Crouch);
            }
            else if(moveDirection == Vector3.zero)
            {
                moveState.SetState(PlayerMoveState.MoveState.Idle);
            }
            else
            {
                currentMoveSpeed = normalSpeed;
                moveState.SetState(PlayerMoveState.MoveState.Move);
            }
        }

        private void MoveController()
        {
            grounded = controller.isGrounded;

            GetInput();
            ChangeMoveSpeed();

            moveDirection *= currentMoveSpeed * speedMultiplier;

            if (grounded)
            {
                yVelocity = -1;
                if (Input.GetKeyDown(SazInput.sazKeyInputData.KeyJump) && !GetHitAbove(normalControllerScaleY + controllerScaleRayOffsetY))
                {
                    yVelocity = jumpSpeed;
                    Crouch(false);
                }
            }
            else
            {
                // If surface above head is hit from jumping, reset controller velocity to avoid floating.
                if (GetHitAbove(transform.localScale.y + controllerScaleRayOffsetY))
                    yVelocity += gravity * Time.deltaTime;
            }

            yVelocity += gravity * Time.deltaTime;
            moveDirection.y = yVelocity;
            controller.Move(moveDirection * Time.deltaTime);

            Debug.DrawRay(transform.position, transform.up * (transform.localScale.y + 0.2F));
        }

        private void Crouch(bool crouch)
        {
            if(crouching && GetHitAbove(normalControllerScaleY + controllerScaleRayOffsetY))
                Debug.Log("Can't stand up, surface block above player.");
            else
                crouching = crouch;
                SetCrouchHeight();
        }

        private void SetCrouchHeight()
        {
            transform.localScale = crouching ?
                new Vector3(transform.lossyScale.x, crouchControllerScaleY, transform.lossyScale.z) :
                new Vector3(transform.lossyScale.x, normalControllerScaleY, transform.lossyScale.z);
        }

        private bool GetHitAbove(float distance)
        {
            return Physics.Raycast(transform.position, Vector3.up, distance);
        }

        private void OnGUI()
        {
            if (!debug)
                return;

            GUI.color = Color.green;
            GUI.Label(new Rect(12, 12, 200, 30), "MoveDirection: " + moveDirection);
        }
    }
}