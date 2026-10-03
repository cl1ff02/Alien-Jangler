using UnityEngine;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    //Prefab Settings
    public GameObject swarmerPrefab;

    //Spawn Group Settings
    public int minGroupSize = 3;
    public int maxGroupSize = 5;

    //Maximum number of active swarm groups allowed in the scene at the same time
    public int maxActiveGroups = 3;

    public float spawnInterval = 5f;
    public float spawnRadius = 3f;
    private float timer = 0f;

    // Track active swarm groups using lists
    private List<List<GameObject>> activeGroups = new List<List<GameObject>>();

    void Update()
    {
        CleanupGroups();
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            if (activeGroups.Count < maxActiveGroups)
            {
                SpawnSwarmerGroup();
            }
            timer = 0f;
        }
    }

    void SpawnSwarmerGroup()
    {
        if (swarmerPrefab == null)
        {
            Debug.LogWarning("Swarmer Prefab is not assigned in EnemySpawn!");
            return;
        }

        int groupSize = Random.Range(minGroupSize, maxGroupSize + 1);
        List<GameObject> currentGroup = new List<GameObject>();

        for (int i = 0; i < groupSize; i++)
        {
            float randomX = Random.Range(-spawnRadius, spawnRadius);
            float randomZ = Random.Range(-spawnRadius, spawnRadius);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 0f, randomZ);

            GameObject newSwarmer = Instantiate(swarmerPrefab, spawnPosition, Quaternion.identity);
            currentGroup.Add(newSwarmer);
        }

        // Add this new group to active groups tracking
        activeGroups.Add(currentGroup);
        Debug.Log($"Spawned a Swarmer group of {groupSize} enemies. Current Active Groups: {activeGroups.Count}/{maxActiveGroups}");
    }

    void CleanupGroups()
    {
        for (int i = activeGroups.Count - 1; i >= 0; i--)
        {
            activeGroups[i].RemoveAll(enemy => enemy == null);

            if (activeGroups[i].Count == 0)
            {
                activeGroups.RemoveAt(i);
            }
        }
    }
}
