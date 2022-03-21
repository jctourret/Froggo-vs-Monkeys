using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private bool stopPlayer = false;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool inAir;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float jumpForce = 2f;

    [Header("Animations")]
    [SerializeField] private GameObject visualObj;
    [SerializeField] private float flipSpeed = 1f;

    [Header("Actions")]
    [SerializeField] private GameObject mouth;
    [SerializeField] private GameObject tongueObj;
    [SerializeField] private float tongueDistance = 2f;
    [SerializeField] private float tongueSpeed = 1f;

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

            ActivateTongueAnim();
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
    #region Movement
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
    #endregion

    #region Animations
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
    #endregion

    #region Actions
    void ActivateTongueAnim()
    {
        if (Input.GetButtonDown("Tongue"))
        {
            mouth.SetActive(true);
            tongueObj.SetActive(false);
            animator.SetBool("TongueAction", true);
        }
    }

    enum State
    {
        start,
        returning,
        exit
    };

    public IEnumerator TongueMovement()
    {
        float time = 0f;

        State state = State.start;

        Vector3 desiredPos = transform.position + (visualObj.transform.right * tongueDistance);
        Vector3 initialPos = tongueObj.transform.position;

        tongueObj.SetActive(true);

        while (state != State.exit)
        {
            if(state != State.returning)
                time += Time.deltaTime * tongueSpeed;
            else
                time -= Time.deltaTime * tongueSpeed;

            tongueObj.transform.position = Vector3.Lerp(initialPos, desiredPos, time);

            if (time > 1)
            {
                state = State.returning;
            }
            else if (time < 0)
            {
                state = State.exit;
            }
            
            yield return null;
        }

        mouth.SetActive(false);
        //tongueObj.SetActive(false);
        animator.SetBool("TongueAction", false);
    }

    #endregion
    // --------------------------------------

    public void StopPlayer(bool state)
    {
        stopPlayer = state;
    }
}