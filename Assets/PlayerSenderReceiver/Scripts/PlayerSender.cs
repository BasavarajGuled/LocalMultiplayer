using UnityEngine;

public class PlayerSender : MonoBehaviour
{
    public PlayerReceiver receiver; // Reference to the receiver
    public PlayerGroundCheck groundCheck;

    public float moveSpeed = 5f;    // WASD speed
    public float scale = 100f;      // Steps per unit (100 = 0.01 precision)

    public static bool isMoving = false;

    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private float jumpForce;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        receiver = FindFirstObjectByType<PlayerReceiver>();
        groundCheck = GetComponent<PlayerGroundCheck>();
    }

    short CompressFloatSigned(float value)
    {
        // Directly scale and cast to short
        return (short)(value * scale);
    }

    void Update()
    {
        HandleMovement();
        Jump();

        // if (!isMoving) return;
        Vector3 pos = transform.position;

        short x = CompressFloatSigned(pos.x);
        short y = CompressFloatSigned(pos.y);
        short z = CompressFloatSigned(pos.z);

        Debug.Log($"Sending position: {pos} | Data size: {sizeof(short) * 3 * 8} bits");

        // Simulate sending to receiver
        receiver.ReceiveCompressedPosition(x, y, z);
    }

    void HandleMovement()
    {
        float h = 0, v = 0;

        // if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        // {
        //     isMoving = true;
        // }
        // else
        // {
        //     isMoving = false;
        //     return; // Skip movement if not moving
        // }
        if (Input.GetKey(KeyCode.W)) v = 1;
        if (Input.GetKey(KeyCode.S)) v = -1;
        if (Input.GetKey(KeyCode.A)) h = -1;
        if (Input.GetKey(KeyCode.D)) h = 1;

        Vector3 move = new Vector3(h, 0, v).normalized * moveSpeed * Time.deltaTime;
        transform.position += move;
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.isGrounded)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            receiver.RecieveForce(Vector3.up * jumpForce);
        }
    }
}
