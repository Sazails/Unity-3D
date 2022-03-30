using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 moveDir;
    float speed, normalSpeed = 3f, sprintSpeed = 6f, jumpForce = 4f, gravity = 15f;
    bool crouchToggle = false;
    bool canCrouch = true;

    CharacterController controller;
    RaycastHit hit;

    void Start()
    {
        speed = normalSpeed;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        #region Movement
        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;

            if (Input.GetButtonDown("Jump"))
            {
                if (!Physics.Raycast(transform.position, Vector3.up, out hit, 1.5f))
                {
                    moveDir.y = jumpForce;
                }
            }
        }

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
        #endregion

        #region Sprinting
        if (Input.GetKey(KeyCode.W) && Input.GetButton("Sprint"))
        {
            Debug.Log("Speed: " + speed);
            if (speed < sprintSpeed)
            {
                speed += .5f;
            }
        }
        else
        {
            speed = normalSpeed;
        }
        #endregion

        #region Crouching
        if (Input.GetButtonDown("Crouch") && canCrouch)
        {
            Crouch();
        }
        #endregion
    }

    void Crouch()
    {
        crouchToggle = !crouchToggle;
        if (crouchToggle)
        {
            transform.localScale = new Vector3(1, .5f, 1);
            controller.height = .4f;
        }
        else if (!crouchToggle)
        {
            if (!Physics.Raycast(transform.position, Vector3.up, out hit, 1.5f))
            {
                transform.localScale = new Vector3(1, 1, 1);
                controller.height = 2f;
            }
        }
    }
}
