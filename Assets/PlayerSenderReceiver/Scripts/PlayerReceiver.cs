using UnityEngine;

public class PlayerReceiver : MonoBehaviour
{
    public float smoothSpeed = 5f;   // Smooth movement speed
    public float scale = 100f;       // Must match sender's scale
    public Vector3 remoteOffset = new Vector3(10, 0, 0); // Keep remote separate in scene

    Vector3 targetPosition;
    [SerializeField]
    private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    float DecompressShortSigned(short value)
    {
        return value / scale;
    }

    public void ReceiveCompressedPosition(short x, short y, short z)
    {
        float rx = DecompressShortSigned(x);
        float ry = DecompressShortSigned(y);
        float rz = DecompressShortSigned(z);

        targetPosition = new Vector3(rx, ry, rz);

        Debug.Log($"Received position: {targetPosition}");
    }

    void Update()
    {
        //if (!PlayerSender.isMoving) return; // Skip if sender is moving
        transform.position = Vector3.Lerp(transform.position, targetPosition + remoteOffset, Time.deltaTime * smoothSpeed);
    }

    public void RecieveForce(Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
