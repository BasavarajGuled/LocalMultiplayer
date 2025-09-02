using UnityEngine;

public abstract class ObstacleCollectable : MonoBehaviour
{
    protected virtual void OnBecameInvisible()
    {
        gameObject.SetActive(false); // Return to pool
    }

    protected abstract void HandleCollision(UnityEngine.Collider other);

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        HandleCollision(other);
    }
}
