using UnityEngine;
using System.Collections;

public class PickUpSpawner : MonoBehaviour 
{

    // He create "New game object" called _enemyspawner
    // This is what runs the enemy spawning script
    // Creates empty game objects called "spawnpoints"
    // drag and drop all spawnpoints into the array (in unity)

    public GameObject[] SpawnPoints;
    public Transform enemyPrefab;   // Drag enemy prefab (in unity)

    public static int TotalPickUps = 0;
    public static int CurrentPickUps = 0;

    public float secondsToSpawn = .5f;
    public static bool Spawning = true;

    // Use this for initialization
    void Start()
    {
        TotalPickUps = Random.Range(10, 20);
        StartCoroutine(SpawnPickUps());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Coroutines must return IENUM
    IEnumerator SpawnPickUps()
    {
        if (CurrentPickUps != TotalPickUps && Spawning == true)
        {
            for (int i = 0; i < TotalPickUps; ++i)
            {
                GameObject.Instantiate(enemyPrefab, SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
            }

            Spawning = false;
            yield return new WaitForSeconds(secondsToSpawn); // "Sleep"
            // yield return new WaitForEndOfFrame(); // "Sleep"
            // yield return new WaitForFixedUpdate(); // "Sleep"
        }
    }
}
