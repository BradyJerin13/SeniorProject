using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class PlayerManager : MonoBehaviour
{
	// This
	public static PlayerManager playerManager;

	public PlayerController playerController;

	// Character Resources
	public CharacterManager[] characterResources;

	// All Character Prefabs
	public ArrayList characterPrefabs;
	
	// Instantiated Character List
	public ArrayList characterList;

	// Selected Character Group
	public ArrayList selectedCharacterGroup;

	// CharacterControllers passed to the PlayerController
	public ArrayList charactercontrollergroup;

	// All of the items the player has (inventory)
	//public ItemManager[] items;

	public PlayerData playerData;

	// Selected character to manage
	public CharacterManager selectedCharacter;


	void Awake()
	{
		// Instantiate new objects
		characterPrefabs = new ArrayList();
		characterList = new ArrayList();
		selectedCharacterGroup = new ArrayList();
		charactercontrollergroup = new ArrayList();

		playerData = new PlayerData();


		if (playerManager == null)
		{
			DontDestroyOnLoad(gameObject);
			playerManager = this;
		}
		else if (playerManager != this)
		{
			Destroy(gameObject);
		}

		// Load characters from resources
		characterResources = (Resources.LoadAll<CharacterManager>("Characters"));

		// Add them to the prefab arraylist
		foreach (CharacterManager character in characterResources)
		{
			characterPrefabs.Add(character);
		}

		PlayerManager.playerManager.LoadPlayerData();

		// Debug
		//foreach (CharacterManager character in characterPrefabs)
		//{
		//	Debug.Log(character.ToString());
		//}
	}

	/// <summary>
	/// Initializes the player controller objects and spawns the characters for play.
	/// </summary>
	public void InitializePlayer()
	{
		PlayerManager.playerManager.characterList.Clear();
		PlayerManager.playerManager.charactercontrollergroup.Clear();

		// Get the player controller
		PlayerManager.playerManager.playerController = PlayerManager.playerManager.GetComponent<PlayerController>();

		// Spawn characters from prefabs
		PlayerManager.playerManager.characterList = LevelManager.levelManager.SpawnCharacters(PlayerManager.playerManager.selectedCharacterGroup);

		Debug.Log(PlayerManager.playerManager.characterList.Count);

		// Pull the character controllers out of the objects
		foreach (CharacterManager character in PlayerManager.playerManager.characterList)
		{
			PlayerManager.playerManager.charactercontrollergroup.Add(character.GetComponent<CharacterController>());
		}

		Debug.Log(PlayerManager.playerManager.charactercontrollergroup.Count);
		// Give the player controller the character controllers
		PlayerManager.playerManager.playerController.FillCharacterGroup(PlayerManager.playerManager.charactercontrollergroup);
	}

	public void UpdatePlayerLevel()
	{
		int count = 0;
		int level = 0;

		foreach (CharacterManager character in PlayerManager.playerManager.characterList)
		{
			level += character.characterData.level;
			++count;
		}
		level = level / count;
		PlayerManager.playerManager.playerData.level = level;
	}

	public void InitializePlayerData(PlayerData data)
	{
		
	}

	public void AddCharacterToGroup(int index)
	{
		PlayerManager.playerManager.selectedCharacterGroup.Add((CharacterManager)PlayerManager.playerManager.characterPrefabs[index]);
	}

	public void RemoveCharacterFromGroup(int index)
	{
		PlayerManager.playerManager.selectedCharacterGroup.RemoveAt(index);
	}

	public void ClearLists()
	{
		PlayerManager.playerManager.characterList.Clear();
		PlayerManager.playerManager.selectedCharacterGroup.Clear();
		PlayerManager.playerManager.charactercontrollergroup.Clear();
	}


	public void SelectCharacter(int index)
	{
		//save?
		PlayerManager.playerManager.SavePlayerData();


		// clear
		//if (selectedCharacter != null)
		//{
		//	Destroy(selectedCharacter);
		//	selectedCharacter = null;
		//}
		PlayerManager.playerManager.selectedCharacter = (CharacterManager)characterList[index];
		PlayerManager.playerManager.selectedCharacter.InitializeCharacter();
	}


	public void ReceiveRewards(float experience, float currency, ArrayList items)
	{
		foreach (CharacterManager character in PlayerManager.playerManager.characterList)
		{
			character.characterData.AddExperience(experience);
			character.SaveCharacterData();
		}

		playerData.wallet += currency;

		foreach (Item item in items)
		{
			PlayerManager.playerManager.playerData.inventory.Add(item);
		}

		PlayerManager.playerManager.SavePlayerData();
	}

	public void RemoveCurrency(float currency)
	{
		if (playerManager.playerData.wallet - currency > 0)
		{
			playerManager.playerData.wallet -= currency;
		}
		SavePlayerData();
	}

	public void AddCurrency(float currency)
	{
		playerManager.playerData.wallet += currency;
		SavePlayerData();
	}

	public void AddItem(Item item)
	{
		PlayerManager.playerManager.playerData.inventory.Add(item);
		SavePlayerData();
	}

	public void RemoveItem(Item item)
	{
		PlayerManager.playerManager.playerData.inventory.Remove(item);
		SavePlayerData();
	}

	public void SavePlayerData()
	{
		Debug.Log("Item Count Before Save = " + PlayerManager.playerManager.playerData.inventory.Count.ToString());
		Debug.Log("Wallet Before Save =" + PlayerManager.playerManager.playerData.wallet.ToString());

		// Save the data to a binary file
		if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
		{
			//File.Delete(Application.persistentDataPath + "/PlayerData.gd");
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);

			bf.Serialize(file, PlayerManager.playerManager.playerData);
			file.Close();
		}
		else
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");
			PlayerData data = new PlayerData();
			data = PlayerManager.playerManager.playerData;
			bf.Serialize(file, data);
			file.Close();
		}

		Debug.Log("Item Count After Save = " + PlayerManager.playerManager.playerData.inventory.Count.ToString());
		Debug.Log("Wallet After Save = " + PlayerManager.playerManager.playerData.wallet.ToString());
	}



	public void LoadPlayerData()
	{
		Debug.Log("Item Count Before Load = " + PlayerManager.playerManager.playerData.inventory.Count.ToString());
		Debug.Log("Wallet Before Load = " + PlayerManager.playerManager.playerData.wallet.ToString());
		
		if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
		{
			PlayerManager.playerManager.playerData.inventory.Clear();

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
			
			Debug.Log(file.Length.ToString());
			
			PlayerData data = (PlayerData)bf.Deserialize(file);
			PlayerManager.playerManager.playerData = data;
			file.Close();

		
			//// Clear the player inventory
			//foreach (ItemManager item in PlayerManager.playerManager.inventory)
			//{
			//	Destroy(item);
			//}

			//PlayerManager.playerManager.inventory.Clear();

			//// Fill inventory items
			//foreach (ItemData itemdata in PlayerManager.playerManager.playerData.inventory)
			//{
			//	Debug.Log("str - " + itemdata.attributeData._baseStrength.ToString());
			//	ItemManager clone = (ItemManager)Instantiate((ItemManager)GameManager.gameManager.itemPrefabList[0]);
			//	clone.InitializeItemData(itemdata);
			//	PlayerManager.playerManager.inventory.Add(clone);
			//}

			//// Update wallet
			//wallet = PlayerManager.playerManager.playerData.wallet;


			//// DEBUG
			//foreach (ItemManager item in PlayerManager.playerManager.inventory)
			//{
			//	Debug.Log(item.ToString());
			//}
		}
		Debug.Log("Item Count After Load = " + PlayerManager.playerManager.playerData.inventory.Count.ToString());
		Debug.Log("Wallet After Load = " + PlayerManager.playerManager.playerData.wallet.ToString());
	}


	[System.Serializable]
	public class PlayerData
	{
		public string name;
		public int level;
		public float wallet;
		public ArrayList inventory;

		public PlayerData()
		{
			name = "";
			wallet = 0;
			level = 0;
			inventory = new ArrayList();
		}
	}








	//public void EquipItem(Item item)
	//{
	//	// Check Character inventory < 4
	//	if (PlayerManager.playerManager.selectedCharacter.items.Count < 4)
	//	{
	//		// Remove item from player inventory
	//		PlayerManager.playerManager.playerData.inventory.Remove(item);

	//		// Add item to character inventory
	//		PlayerManager.playerManager.selectedCharacter.items.Add(item);
	//	}
	//	else
	//	{
	//		// Characters inventory is full
	//	}
	//}

	//public void UnEquipItem(Item item)
	//{
	//	// If character has item
	//	if (PlayerManager.playerManager.selectedCharacter.items.Contains(item))
	//	{
	//		// remove from character
	//		PlayerManager.playerManager.selectedCharacter.items.Remove(item);

	//		// add to player
	//		PlayerManager.playerManager.playerData.inventory.Add(item);
	//	}
	//}
}
