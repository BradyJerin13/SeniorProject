using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	// Resources
	public ChunkManager[] chunksResources;
	public EnemyManager[] enemyResources;
	public InteractableManager[] interactableResources;

	// All enemies
	public ArrayList enemyPrefabsList;
	public ArrayList enemyList;

	// All Bosses
	public ArrayList bossPrefabsList;
	public ArrayList bossList;

	// All Interactables
	public ArrayList interactablePrefabsList;
	public ArrayList interactableList;

	// All persons (NPC's)
	public ArrayList personPrefabsList;
	public ArrayList personList;

	// Enemy Chunks
	public ArrayList chunkPrefabsList;
	public ArrayList chunkList;

	// Start Chunk
	public ChunkManager startChunkPrefab;
	public ChunkManager startChunk;

	// Start Chunks
	public ChunkManager endChunkPrefab;
	public ChunkManager endChunk;
	public InteractableManager endPortalPrefab;
	public InteractableManager endPortal;

	// Boss Chunks
	public ArrayList bossChunkPrefabsList;
	public ArrayList bossChunkList;

	// Static instance of itself
	public static LevelManager levelManager;

	public int EnemyKillCount;
	public int EnemyKillTotal;

	public int BossKillCount;
	public int BossKillTotal;

	public int CharacterDeathCount;

	public int difficulty;
	public int chunkTotal;


	void Awake()
	{
		chunkPrefabsList = new ArrayList();
		chunkList = new ArrayList();

		bossChunkPrefabsList = new ArrayList();
		bossChunkList = new ArrayList();

		enemyPrefabsList = new ArrayList();
		enemyList = new ArrayList();

		bossPrefabsList = new ArrayList();
		bossList = new ArrayList();

		interactablePrefabsList = new ArrayList();
		interactableList = new ArrayList();

		personPrefabsList = new ArrayList();
		personList = new ArrayList();

		if (levelManager == null)
		{
			DontDestroyOnLoad(gameObject);
			levelManager = this;
		}
		else if (levelManager != this)
		{
			Destroy(gameObject);
		}

		// Gather game objects from resources
		chunksResources = (Resources.LoadAll<ChunkManager>("Chunks"));
		enemyResources = (Resources.LoadAll<EnemyManager>("Enemies"));
		interactableResources = (Resources.LoadAll<InteractableManager>("Interactables"));

		// Move the resources into array lists
		// Chunks
		foreach (ChunkManager chunk in chunksResources)
		{
			if (chunk.name.Contains("StartChunk"))
			{
				startChunkPrefab = chunk;
			}
			else if (chunk.name.Contains("EndChunk"))
			{
				endChunkPrefab = chunk;
			}
			else if (chunk.name.Contains("BossChunk"))
			{
				bossChunkPrefabsList.Add(chunk);
			}
			else
			{
				chunkPrefabsList.Add(chunk);
			}
		}

		// Enemies
		foreach (EnemyManager enemy in enemyResources)
		{
			if (enemy.name.Contains("Boss"))
			{
				bossPrefabsList.Add(enemy);
			}
			else if (enemy.name.Contains("Enemy"))
			{
				enemyPrefabsList.Add(enemy);
			}
		}

		// Interactables
		foreach (InteractableManager interactable in interactableResources)
		{
			if (interactable.name.Contains("Person"))
			{
				personPrefabsList.Add(interactable);
			}
			else if (interactable.name.Contains("EndPortal"))
			{
				endPortalPrefab = interactable;
			}
			else
			{
				interactablePrefabsList.Add(interactable);
			}
		}
	}

	public void SetDifficulty(int value)
	{
		LevelManager.levelManager.difficulty = value;
	}


	/// <summary>
	/// Generates the entire level with multiple random elements
	/// Then fills the level
	/// </summary>
	public void InitializeLevel()
	{
		LevelManager.levelManager.chunkTotal = Random.Range(10, 20);

		Vector3 instantiatePosition = new Vector3(0, 0, 0);
		Vector3 lastInstantiatePosition = new Vector3(0, 0, 0);

		// Instantiate the start chunk
		startChunk = (ChunkManager)Instantiate(startChunkPrefab, instantiatePosition, startChunkPrefab.transform.rotation);

		// Random Generate map
		for (int i = 0; i < LevelManager.levelManager.chunkTotal - 3; ++i)
		{

			if (instantiatePosition.z < 20)
			{
				instantiatePosition.z += 10;
			}
			else
			{
				int random = Random.Range(0, 100);

				if (random < 70)
				{
					lastInstantiatePosition.x = 1;
					instantiatePosition.z += 10;
				}
				else
				{
					random = Random.Range(0, 100);

					if (random > 50)
					{
						if (((instantiatePosition.x - 10) != lastInstantiatePosition.x))
						{
							lastInstantiatePosition = instantiatePosition;
							instantiatePosition.x -= 10;
						}
						else if (((instantiatePosition.x + 10) != lastInstantiatePosition.x))
						{
							lastInstantiatePosition = instantiatePosition;
							instantiatePosition.x += 10;
						}
					}
					else if (random <= 50)
					{
						if (((instantiatePosition.x + 10) != lastInstantiatePosition.x))
						{
							lastInstantiatePosition = instantiatePosition;
							instantiatePosition.x += 10;
						}
						else if (((instantiatePosition.x - 10) != lastInstantiatePosition.x))
						{
							lastInstantiatePosition = instantiatePosition;
							instantiatePosition.x -= 10;
						}
					}
					else
					{
						lastInstantiatePosition.x = 1;
						instantiatePosition.z += 10;
					}

				}

			}

			if (i % 5 == 0 && i != 0)
			{
				ChunkManager randomchunk = RandomSelectChunk(bossChunkPrefabsList);
				chunkList.Add(Instantiate(randomchunk, instantiatePosition, randomchunk.transform.rotation));
			}
			else
			{
				ChunkManager randomchunk = RandomSelectChunk(chunkPrefabsList);
				chunkList.Add(Instantiate(randomchunk, instantiatePosition, randomchunk.transform.rotation));
			}
		}

		instantiatePosition.z += 10;

		//// Instantiate enemy chunks, add them to the active chunk list
		//foreach (ChunkManager chunk in chunkPrefabsList)
		//{
		//	chunkList.Add(Instantiate(chunk, instantiatePosition, chunk.transform.rotation));
		//}

		// boss chunks
		//foreach (ChunkManager chunk in bossChunkPrefabsList)
		//{
		//	chunkList.Add(Instantiate(chunk, instantiatePosition, chunk.transform.rotation));
		//	instantiatePosition.z += 10;
		//}

		endChunk = (ChunkManager)(Instantiate(endChunkPrefab, instantiatePosition, endChunkPrefab.transform.rotation));

		foreach (Transform point in endChunk.interactablePoints)
		{
			endPortal = (InteractableManager)Instantiate(endPortalPrefab, point.position, endPortalPrefab.transform.rotation);
		}

		// Spawn
		foreach (ChunkManager chunk in chunkList)
		{
			// Spawn Enemies
			foreach (Transform point in chunk.spawnPoints)
			{
				EnemyManager random = RandomSelectEnemy(enemyPrefabsList);
				EnemyManager clone = (EnemyManager)Instantiate(random, point.position, point.rotation);
				clone.UpdateEnemy(difficulty);
				enemyList.Add(clone);
			}

			// Spawn Bosses
			foreach (Transform point in chunk.bossPoints)
			{
				EnemyManager random = RandomSelectEnemy(bossPrefabsList);
				EnemyManager clone = (EnemyManager)Instantiate(random, point.position, point.rotation);
				clone.UpdateEnemy(difficulty);
				enemyList.Add(clone);
			}

			// Spawn Interactables
			foreach (Transform point in chunk.interactablePoints)
			{
				InteractableManager random = RandomSelectInteractable(interactablePrefabsList);
				interactableList.Add(Instantiate(random, point.position, point.rotation));
			}

			foreach (Transform point in chunk.personPoints)
			{
				InteractableManager random = RandomSelectInteractable(personPrefabsList);
				interactableList.Add(Instantiate(random, point.position, point.rotation));
			}
		}

		// Set up objectives
		LevelManager.levelManager.EnemyKillTotal = LevelManager.levelManager.enemyList.Count;
		LevelManager.levelManager.EnemyKillCount = 0;

		LevelManager.levelManager.BossKillTotal = LevelManager.levelManager.bossList.Count;
		LevelManager.levelManager.BossKillCount = 0;

		LevelManager.levelManager.CharacterDeathCount = 0;
	}


	public ArrayList SpawnCharacters(ArrayList characters)
	{
		ArrayList characterclones = new ArrayList();

		// Instantiate characters
		for (int i = 0; i < characters.Count && i < startChunk.spawnPoints.Count; ++i)
		{
			CharacterManager clone = (CharacterManager)Instantiate((CharacterManager)characters[i], ((Transform)startChunk.spawnPoints[i]).position, ((CharacterManager)characters[i]).transform.rotation);
			clone.LoadCharacterData();
			clone.UpdateCharacter();

			characterclones.Add(clone);
		}

		// Instantiate non playable characters
		foreach (Transform point in startChunk.personPoints)
		{
			InteractableManager random = RandomSelectInteractable(personPrefabsList);
			enemyList.Add(Instantiate(random, point.position, point.rotation));
		}

		// Debug
		//foreach (CharacterManager character in characterclones)
		//{
		//	Debug.Log(character.ToString());
		//}

		return characterclones;
	}


	public ChunkManager RandomSelectChunk(ArrayList chunks)
	{
		int random = Random.Range(0, chunks.Count);
		return (ChunkManager)chunks[random];
	}

	public EnemyManager RandomSelectEnemy(ArrayList enemies)
	{
		int random = Random.Range(0, enemies.Count);
		return (EnemyManager)enemies[random];
	}

	public InteractableManager RandomSelectInteractable(ArrayList interactables)
	{
		int random = Random.Range(0, interactables.Count);
		return (InteractableManager)interactables[random];
	}


	public void LevelComplete()
	{
		float experience = LevelManager.levelManager.EnemyKillCount * 7 * LevelManager.levelManager.difficulty;
		float currency = LevelManager.levelManager.EnemyKillCount * 1 * LevelManager.levelManager.difficulty;
		ArrayList loot = new ArrayList();

		Debug.Log("ENEMY OBJECTIVES" + EnemyKillCount.ToString() + "/" + EnemyKillTotal.ToString());
		Debug.Log("ENEMY OBJECTIVES" + enemyList.Count);


		// Account for objectives completed
		if (LevelManager.levelManager.EnemyKillCount == LevelManager.levelManager.EnemyKillTotal)
		{
			Item temp = new Item(5);
			loot.Add(temp);
		}

		if (LevelManager.levelManager.BossKillCount == LevelManager.levelManager.BossKillTotal)
		{
			Item temp = new Item(5);
			loot.Add(temp);
		}

		if (LevelManager.levelManager.CharacterDeathCount == 0)
		{
			Item temp = new Item(5);
			loot.Add(temp);
		}

		for (int i = 0; i < LevelManager.levelManager.difficulty; ++i)
		{
			Item temp = new Item(5);
			loot.Add(temp);
		}

		PlayerManager.playerManager.ReceiveRewards(experience, currency, loot);
		MenuManager.menuManager.FillRewardsPanel(experience, currency, loot);

		chunkList.Clear();
		bossChunkList.Clear();
		bossList.Clear();
		enemyList.Clear();
		interactableList.Clear();
		personList.Clear();
		PlayerManager.playerManager.ClearLists();
	}
}
