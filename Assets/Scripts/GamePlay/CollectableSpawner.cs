using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public ObjectPool pool;
    public Transform player;
    public float spawnInterval = 2f;
    public float spawnDistance = 55f;
    public float yPosition = 3f;

    public float offset = -10f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnCollectible), 0f, spawnInterval);
    }

    void SpawnCollectible()
    {
        GameObject collectibleOne = pool.GetObject();
        collectibleOne.SetActive(true);
        GameObject collectibleTwo = pool.GetObject();
        collectibleTwo.SetActive(true);


        float randomX = Random.Range(2, 9f);
        collectibleOne.transform.position = new Vector3(randomX, yPosition, player.position.z + spawnDistance);
        collectibleTwo.transform.position = new Vector3(randomX + offset, yPosition, player.position.z + spawnDistance);

    }
}
