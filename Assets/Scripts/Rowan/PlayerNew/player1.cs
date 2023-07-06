using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player1 : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    // [SerializeField] float acceleration = 18f;

    public float groundDrag;



    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    public float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;


    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.2f;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    public RaycastHit slopeHit;
    public bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    static public bool dialogue = false;

    public MovementState state; // always stores the current state the player is in

    public enum MovementState 
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y; //saves the local Y scale of the player
    }

    private void Update()
    {
        // ground check
        // grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround); //maak  van 0.1f een nieuwe float [SerializeField] float groundDistance = 0.2f; en noem het hier
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

        

        MyInput();
        SpeedControl();
        StateHandler();
        Gravity();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        if (!dialogue)
        {
            MovePlayer();
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // start crouch
        if(Input.GetKeyDown(crouchKey)) //if crouchKey pressed schrink player down by settling local scale to new vector3 keep x & z te same but change  the y scale to your crouchYScale
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            // rb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // add gravity when player crouched reason for this is when player crouches he wil be in mid air so we apply force to player so it wil go ground fast
        }

        // stop crouch
        if(Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {   
        // mode - crouching
        if (Input.GetKey(crouchKey)) // if presiing crouchKey we want to set the state from  Walking to crouching 
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        // mode - sprinting
        else if (grounded && Input.GetKey(sprintKey)) // if player is grounded and is presiing sprintkey we want to set the state Walking to Sprinting
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        //mode - walking 
        else if (grounded) //if player is grounded but not pressing sprint set state to walkSpeed
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else // if player is not grounded  and not pressing sprint set state to air
        {
            state = MovementState.air;
        }
    }

    void Gravity()
    {
        // rb.AddForce(0, -gravity, 0);
        rb.AddForce(Physics.gravity);
    }


    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if(OnSlope() && !exitingSlope) //if the player is on a Slope 
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force); // adforce in the Slope MoveDirection

            if (rb.velocity.y > 0) // if player is moving upwords
                rb.AddForce(Vector3.down * 80f, ForceMode.Force); // then we want to add downword force so the player wil constantly on the slope & wont be bymping
        }
        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

            // turn gravity off while on slope
            rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {   
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if(rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        // reset y velocity
        //rb.velocity = new Vector3(rb.velocity.x, 5f, rb.velocity.z); // consistant jumping same heigt every time 

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); 
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);     //en dit zorg for inconsistant jumping dus soms spring je hoger dan een andere keer
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.2f)) // out slopeHit this stores the object that we hit in the slopeHit Variable
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.blue); // !!!!!! testing pruposes

            float angle = Vector3.Angle(Vector3.up, slopeHit.normal); // calculate how steep the slope is 
            return angle < maxSlopeAngle && angle != 0; // we want the bool to return true if the angle is smaller then oure maxSlope Angle & not 0
        }

        return false; // if the raycast doe not hit annyhing return false 
    }

    private Vector3 GetSlopeMoveDirection() // project oure moveDirection with the angle of the slope(if slope 40degrees moveDirection wil be 40degrees) os you wont walk in the slope but on it 
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}