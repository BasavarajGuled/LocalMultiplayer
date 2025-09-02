using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerSender : MonoBehaviour
{
    public PlayerReceiver receiver;   // Reference to the receiver
    public PlayerGroundCheck groundCheck;

    public float moveSpeed = 5f;      // WASD movement speed
    public float scale = 100f;        // Steps per unit for compression
    public float jumpForce = 5f;      // Jump impulse force

    public static bool isMoving = false;

    private Rigidbody rb;
    private bool jumpRequested = false;
    private bool isPlayerFell = false;
    private Vector3 moveInput = Vector3.zero;

    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f; // Seconds allowed between taps

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
        SendCompressedPosition();
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

            // Handle movement ONLY on the right half of the screen
            if (touch.position.x > Screen.width * 0.5f)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    if (touch.position.x > Screen.width * 0.75f)
                        h = 1f;  // Far right -> move right
                    else
                        h = -1f; // Closer to center -> move left
                }
            }

            // Detect double-tap for jump
            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time - lastTapTime < doubleTapThreshold && groundCheck.isGrounded)
                {
                    jumpRequested = true; // Double-tap detected
                }
                lastTapTime = Time.time;
            }
        }
#endif

        moveInput = new Vector3(h, 0f, 1f).normalized * moveSpeed;
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
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            receiver.RecieveForce(Vector3.up * jumpForce);
            jumpRequested = false;
        }
    }

    void SendCompressedPosition()
    {
        Vector3 pos = transform.position;

        short x = CompressFloatSigned(pos.x);
        short y = CompressFloatSigned(pos.y);
        short z = CompressFloatSigned(pos.z);

        Debug.Log($"Sending position: {pos} | Data size: {sizeof(short) * 3 * 8} bits");

        // Simulate sending to receiver
        receiver.ReceiveCompressedPosition(x, y, z);
    }

    short CompressFloatSigned(float value)
    {
        return (short)(value * scale);
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
