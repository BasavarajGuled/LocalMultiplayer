using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPool pool;
    public Transform player;
    public float spawnInterval = 3f;
    public float spawnDistance = 55f;
    public float yPosition = 0.5f;
    public float offset = -10f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacles), 0f, spawnInterval);
    }

    void SpawnObstacles()
    {
        GameObject obstacle1 = pool.GetObject();
        obstacle1.SetActive(true);

        GameObject obstacle2 = pool.GetObject();
        obstacle2.SetActive(true);

        float baseX = Random.Range(2f, 9f);

        obstacle1.transform.position = new Vector3(baseX, yPosition, player.position.z + spawnDistance);
        obstacle2.transform.position = new Vector3(baseX + offset, yPosition, player.position.z + spawnDistance);
    }
}
