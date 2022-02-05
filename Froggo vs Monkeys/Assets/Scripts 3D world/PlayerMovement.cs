using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool stopPlayer = false;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool inAir;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float jumpForce = 2f;

    [Header("Animations")]
    [SerializeField] private GameObject visualObj;
    [SerializeField] private float flipSpeed = 1f;

    // -----------------------------------

    CharacterController controller;
    Animator animator;
    Vector3 playerVelocity;

    bool left;
    bool right;
    bool lastDirection = true;

    static float _ROTATION = 180;

    float leftNumber = _ROTATION;
    float rightNumber = _ROTATION;

    // -----------------------------------

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = visualObj.GetComponent<Animator>();
    }

    void Start()
    {
        isGrounded = false;
        inAir = false;
    }

    void Update()
    {
        if (!stopPlayer)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityValue);
            }

            FlipSprite();
        }
    }

    private void FixedUpdate()
    {
        if (!stopPlayer)
        {
            Move();
        }
    }

    // --------------------------------------------------

    private void Move()
    {
        isGrounded = controller.isGrounded;

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;

            if (inAir)
            {
                inAir = false;
            }
        }
        else
        {
            inAir = true;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        move = Vector3.ClampMagnitude(move, 1);

        animator.SetFloat("VelocityX", move.x);
        animator.SetFloat("VelocityZ", move.z);

        animator.SetFloat("Magnitude", move.magnitude);

        controller.Move(move * speed * Time.deltaTime);

        playerVelocity.y += gravityValue * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void FlipSprite()
    {
        if (Input.GetKeyDown("a") || left == true)
        {
            if (lastDirection == true)
            {
                if (leftNumber > 0)
                {
                    visualObj.transform.Rotate(new Vector3(0f, flipSpeed, 0f) * Time.deltaTime);
                    left = true;

                    leftNumber -= flipSpeed * Time.deltaTime;
                }
                else
                {
                    left = false;
                    leftNumber = _ROTATION;

                    visualObj.transform.eulerAngles = new Vector3(0, 180, 0);

                    lastDirection = false;
                }
            }
        }
        else if (Input.GetKeyDown("d") || right == true)
        {
            if (lastDirection == false)
            {
                if (rightNumber > 0)
                {
                    visualObj.transform.Rotate(new Vector3(0f, -flipSpeed, 0f) * Time.deltaTime);
                    right = true;

                    rightNumber -= flipSpeed * Time.deltaTime;
                }
                else
                {
                    right = false;
                    rightNumber = _ROTATION;

                    visualObj.transform.eulerAngles = new Vector3(0, 0, 0);

                    lastDirection = true;
                }
            }
        }
    }

    // --------------------------------------

    public void StopPlayer(bool state)
    {
        stopPlayer = state;
    }
}