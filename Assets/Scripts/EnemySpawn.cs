using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 5f;
    public int packSize = 3;
    public float spawnRadius = 2f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPack();
            timer = 0f;
        }
    }

    void SpawnPack()
    {
        if (enemyPrefab == null) return;

        for (int i = 0; i < packSize; i++)
        {
            float randomX = Random.Range(-spawnRadius, spawnRadius);
            float randomZ = Random.Range(-spawnRadius, spawnRadius);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 0f, randomZ);

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
