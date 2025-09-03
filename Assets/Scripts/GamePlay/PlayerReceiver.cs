using UnityEngine;

public class PlayerReceiver : MonoBehaviour
{
    public Vector3 remoteOffset; // Keep remote separate in scene

    Vector3 targetPosition;
    [SerializeField]
    private new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void ReceivePosition(Vector3 pos)
    {
        targetPosition = pos;
        Debug.Log($"Received position: {targetPosition}");
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition + remoteOffset, Time.deltaTime * GameManager.Instance.moveSpeed);
    }

    public void RecieveForce(Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.Force);
    }

}
