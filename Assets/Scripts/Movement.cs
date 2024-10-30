                                                                                                                                                                                                                                                                                                                                                                                                                using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
//This script allows the player to control the character's movement and camera direction
public class Movement : MonoBehaviour
{
    //Controls how fast the player is able to walk, run and jump etc. Also controls the range of the camera
    public Camera playerCamera;
    public float walkSpeed = 20f;
    public float runSpeed = 30f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    //Allows the character to be able to move 
    private bool canMove = true;

    void Start()
    {
        //Fully controls the characters input by allowing it to run, walk, crouch, jump etc.  
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        //Allows the player to be able to move in different directions and sprint when holdng the 'shift' button
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //Controls the players jumping action and input by defining their jump power and speed
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        //Allows the player to stay on the ground by turning on gravity when moving
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //Controls the players crouch speed when walking or running and is activated when pressing the 'R' button on the keyboard
        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }

        //Controls how fast the player is able to walk and run as well as the camera height
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 20f;
            runSpeed = 30f;
        }

        characterController.Move(moveDirection * Time.deltaTime);
        //Defines the camera speed and rotation, also how far the limit is for the player to look around
        //The Camera moves around when the player uses the mouse
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}