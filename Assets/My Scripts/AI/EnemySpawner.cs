using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    // He create "New game object" called _enemyspawner
    // This is what runs the enemy spawning script
    // Creates empty game objects called "spawnpoints"
    // drag and drop all spawnpoints into the array (in unity)

    public GameObject[] SpawnPoints;
    public Transform enemyPrefab;   // Drag enemy prefab (in unity)

    public float secondsToSpawn = .5f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Coroutines must return IENUM
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObject.Instantiate(enemyPrefab, SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(secondsToSpawn); // "Sleep"
            // yield return new WaitForEndOfFrame(); // "Sleep"
            // yield return new WaitForFixedUpdate(); // "Sleep"

        }
    }
}
