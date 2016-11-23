using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	public GameObject meleeMob;
	public GameObject rangedMob;
	public GameObject healingMob;
	public int maxSpawns;


	public float spawnWait;
	public Transform spawnPosition;
	public float state = 0;

	private int _currentSpawns;

	void Start()
	{
		StartCoroutine(SpawnWaves());
		_currentSpawns = 0;
	}

	IEnumerator SpawnWaves()
	{
		while (true)
		{
			// Set the Unit Tag to the Buildings Tag
			if (_currentSpawns != maxSpawns)
			{
				if (state == 0)
				{
					Instantiate(meleeMob, spawnPosition.position, spawnPosition.rotation);
				}
				else if (state == 1)
				{
					Instantiate(rangedMob, spawnPosition.position, spawnPosition.rotation);
				}
				else if (state == 2)
				{
					Instantiate(healingMob, spawnPosition.position, spawnPosition.rotation);
				}
			}

			// Spawn unit
			yield return new WaitForSeconds(spawnWait);
		}
	}
}
