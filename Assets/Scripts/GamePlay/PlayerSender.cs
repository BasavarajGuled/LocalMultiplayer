using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerSender : MonoBehaviour
{
    public PlayerReceiver receiver;   // Reference to the receiver
    public PlayerGroundCheck groundCheck;

    public float moveSpeed = 5f;      // WASD movement speed
    public float jumpForce = 5f;      // Jump impulse force

    public static bool isMoving = false;

    private Rigidbody rb;
    private bool jumpRequested = false;
    private bool isPlayerFell = false;
    private Vector3 moveInput = Vector3.zero;

    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f; // Seconds allowed between taps
    private Vector2 touchStartPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Smooth physics

        // Find references
        receiver = FindFirstObjectByType<PlayerReceiver>();
        groundCheck = GetComponent<PlayerGroundCheck>();
    }

    void Update()
    {
        HandleInput();
        SendPosition();
        CheckPlayerPos();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleInput()
    {
        float h = 0f;
#if UNITY_EDITOR
        // WASD movement input
        if (Input.GetKey(KeyCode.LeftArrow)) h = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) h = 1f;

        // Jump input (flag, don’t apply force here)
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.isGrounded)
        {
            jumpRequested = true;
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Detect double-tap for jump
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                if (Time.time - lastTapTime < doubleTapThreshold && groundCheck.isGrounded)
                {
                    jumpRequested = true; // Double-tap detected
                }
                lastTapTime = Time.time;
            }

            // Handle movement ONLY on the right half of the screen
            if (touch.position.x > Screen.width * 0.5f)
            {
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    float deltaX = touch.position.x - touchStartPos.x;

                    if (deltaX > 20f)      // Swiped right
                        h = 1f;
                    else if (deltaX < -20f) // Swiped left
                        h = -1f;
                }
            }
        }
#endif

        moveInput = new Vector3(h, 0f, 1f).normalized * GameManager.Instance.moveSpeed;
    }

    void HandleMovement()
    {
        // Physics-based movement
        Vector3 newPos = rb.position + moveInput * Time.fixedDeltaTime;
        rb.MovePosition(newPos);

        isMoving = moveInput.sqrMagnitude > 0.01f;
    }

    void HandleJump()
    {
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Force);
            receiver.RecieveForce(Vector3.up * jumpForce);
            jumpRequested = false;
        }
    }

    void SendPosition()
    {
        receiver.ReceivePosition(transform.position);
    }

    private void CheckPlayerPos()
    {
        if (transform.position.y < 0f && !isPlayerFell)
        {
            isPlayerFell = true;
            GameManager.Instance.uIController.ShowGameOver();
        }
    }


}
