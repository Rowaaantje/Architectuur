using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    float playerHeight = 2f;

    [SerializeField] Transform orientation;

    [Header("Movement")]
    public float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
//    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 18f;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float runFov;
    [SerializeField] private float runFovTime;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    bool isGrounded;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    [Header("Gravity")]
    [SerializeField] public float gravity;

    Rigidbody rb;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();
        Gravity();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement; //using Transform.right & forward so we move in the direction relitive where the player is looking
    }

    void Jump()
    {   
        if(isGrounded)
        {               
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ControlSpeed()
    {
        float f = fov; // rik
        if (Input.GetKey(sprintKey) && isGrounded && rb.velocity.magnitude > 0.0f)
        {
            f = runFov; //rik
        }

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, f, runFovTime * Time.deltaTime);
        moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
    }

    void Gravity()
    {
        rb.AddForce(0, -gravity, 0);
    }


    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate() //Fixedupdate has the frequencies of the physics system and this scipt movement is Physics based cuz its using and rigidbody this way the movement wil look smooth
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration); //normalized makes it so if you move vertical (W+A || W+D ) you wont move faster  than just W || S
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
}