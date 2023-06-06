using System.Collections;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject[] collectiblePrefabs; // Assign your collectible prefabs in the inspector
    public float spawnInterval = 15f; // The interval between each spawn

    // Define the area where the collectibles can spawn
    public Vector2 minSpawnPosition;
    public Vector2 maxSpawnPosition;

    public int maxCollectibles = 6; // Maximum number of collectibles to spawn
    private int collectiblesSpawned = 0; // Counter for the number of collectibles spawned

    private void Start()
    {
        // Start the spawn coroutine
        StartCoroutine(SpawnCollectibles());
    }

    private IEnumerator SpawnCollectibles()
    {
        while (collectiblesSpawned < maxCollectibles)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(spawnInterval);

            // Generate a random position within the defined area
            Vector2 spawnPosition = new Vector2(
                Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
            );

            // Weighted random selection of a collectible type
            int randomWeight = Random.Range(0, 100);
            GameObject collectiblePrefab;

            if (randomWeight < 50) // 50% chance for HealthCollectible
            {
                collectiblePrefab = collectiblePrefabs[0]; // Assuming HealthCollectible is at index 0
            }
            else if (randomWeight < 75) // 25% chance for SpeedCollectible
            {
                collectiblePrefab = collectiblePrefabs[1]; // Assuming SpeedCollectible is at index 1
            }
            else // 25% chance for ImmunityCollectible
            {
                collectiblePrefab = collectiblePrefabs[2]; // Assuming ImmunityCollectible is at index 2
            }

            // Instantiate the collectible at the generated position
            GameObject newCollectible = Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);

            // Assign a unique ID to the collectible
            string uniqueId = System.Guid.NewGuid().ToString();
            newCollectible.GetComponent<ICollectible>().SetId(uniqueId);

            // Increase the counter
            collectiblesSpawned++;
        }
    }

}
