using UnityEngine;

public class Obstacle : ObstacleCollectable
{
    public delegate void Reload();
    public static event Reload reload;
    protected override void HandleCollision(UnityEngine.Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            reload?.Invoke();
        }
    }
}
