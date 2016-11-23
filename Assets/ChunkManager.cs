using UnityEngine;
using System.Collections;

public class ChunkManager : MonoBehaviour
{
	public ArrayList spawnPoints;
	public ArrayList interactablePoints;
	public ArrayList bossPoints;
	public ArrayList personPoints;

	void Awake()
	{
		spawnPoints = new ArrayList();
		interactablePoints = new ArrayList();
		bossPoints = new ArrayList();
		personPoints = new ArrayList();

		foreach (Transform child in transform)
		{
			if (child.name == "InteractablePoint")
			{
				interactablePoints.Add(child);
			}
			else if (child.name == "SpawnPoint")
			{
				spawnPoints.Add(child);
			}
			else if (child.name == "BossPoint")
			{
				bossPoints.Add(child);
			}
			else if (child.name == "PersonPoint")
			{
				personPoints.Add(child);
			}
		}

		// Debug
		foreach (Transform point in spawnPoints)
		{
			Debug.Log(point.ToString());
		}
	}
}