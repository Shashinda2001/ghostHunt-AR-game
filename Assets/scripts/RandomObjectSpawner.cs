using UnityEngine;
using System.Collections;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab to spawn
    public Transform referenceObject; // The reference object's position
    public float spawnRange = 5f; // Range within which to spawn the object
    public int numberOfObjects = 1; // Number of objects to spawn
    public float spawnDelay = 1f; // Delay in seconds between each spawn

    void Start()
    {
        if (referenceObject == null)
        {
            Debug.LogError("Reference object is not assigned!");
        }

        if (objectToSpawn == null)
        {
            Debug.LogError("Object to spawn is not assigned!");
        }

        // Start the spawning coroutine
        StartCoroutine(SpawnObjectsWithDelay());
    }

    IEnumerator SpawnObjectsWithDelay()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnRandomObject();
            yield return new WaitForSeconds(spawnDelay); // Wait before spawning the next object
        }
    }

    void SpawnRandomObject()
    {
        if (referenceObject == null || objectToSpawn == null) return;

        // Generate a random position within the range of the reference object
        float randomX = Random.Range(-spawnRange, spawnRange);
        //  float randomZ = Random.Range(-spawnRange, spawnRange);
        float randomZ =  spawnRange;


        // Ensure the random Y is not less than the referenceObject's Y value
        float randomY = Random.Range(referenceObject.position.y, referenceObject.position.y + spawnRange);

        Vector3 randomPosition = referenceObject.position + new Vector3(randomX, randomY, randomZ);

        // Instantiate the object at the random position
        Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }
}
