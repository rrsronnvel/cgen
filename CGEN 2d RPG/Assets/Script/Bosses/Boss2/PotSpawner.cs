using System.Collections;
using UnityEngine;

public class PotSpawner : MonoBehaviour
{
    public GameObject potPrefab; // Assign your pot prefab in the inspector
    public float spawnInterval = 15f; // The interval between each spawn

    // Define the area where the pots can spawn
    public Vector2 minSpawnPosition;
    public Vector2 maxSpawnPosition;

    private void Start()
    {
        // Start the spawn coroutine
        StartCoroutine(SpawnPots());
    }

    private IEnumerator SpawnPots()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(spawnInterval);

            // Generate a random position within the defined area
            Vector2 spawnPosition = new Vector2(
                Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
            );

            // Instantiate the pot at the generated position
            Instantiate(potPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
