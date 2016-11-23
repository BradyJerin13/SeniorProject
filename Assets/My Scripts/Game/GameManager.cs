using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
	//
	public static GameManager gameManager;

	// item 
	public ArrayList itemPrefabList;

	void Awake()
	{
		itemPrefabList = new ArrayList();

		if(gameManager == null)
		{
			DontDestroyOnLoad(gameObject);
			gameManager = this;
		}
		else if(gameManager != this)
		{
			Destroy(gameObject);
		}

		ArrayList itemclones = new ArrayList();

		for (int i = 0; i < 3; ++i)
		{
			Item item = new Item(2);
			itemclones.Add(item);
		}

	}

	public void GenerateLevel()
	{
		Application.LoadLevel(1);
	}

	void OnLevelWasLoaded()
	{
		if (Application.loadedLevel == 1)
		{
			LevelManager.levelManager.InitializeLevel();
			PlayerManager.playerManager.InitializePlayer();
			MenuManager.menuManager.InstantiateLevelMenu();
		}
	}


	public void ExitLevel()
	{
		Application.LoadLevel(0);
	}

	public void ExitApplication()
	{
		Application.Quit();
	}

	/// <summary>
	/// REMOVE
	/// </summary>
	/// <param name="i"></param>
	/// <returns></returns>
	public void SelectCharacter(int i)
	{
		PlayerManager.playerManager.SelectCharacter(0);
	}
}


[System.Serializable]
public class Item
{
	public string name;
	public int level;

	public float cost;

	public int strength;
	public int dexterity;
	public int intellect;
	public int will;
	public int toughness;

	public Item(int ilevel)
	{
		name = "default";
		level = ilevel;
		cost = 1;

		strength = 0;
		dexterity = 0;
		intellect = 0;
		will = 0;
		toughness = 0;

		RandomizeAttributes();
	}

	private void RandomizeAttributes()
	{
		strength = Random.Range(0, level);
		dexterity = Random.Range(0, level);
		intellect = Random.Range(0, level);
		will = Random.Range(0, level);
		toughness = Random.Range(0, level);
	}
}