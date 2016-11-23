using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Character Manager Class defines what a character is.
/// </summary>
public class CharacterManager : MonoBehaviour
{
	// Controller for the character
	public CharacterController characterController;

	public CharacterData characterData;

	// Attributes and Statistics
	public Attributes _attributes;
	public StatsOffense _statsOffense;
	public StatsDefense _statsDefense;
	public StatsGeneral _statsGeneral;

	// Abilities
	public GameObject basicAbilityPrefab;
	public GameObject abilityQPrefab;
	public GameObject abilityWPrefab;
	public GameObject abilityEPrefab;
	public GameObject abilityRPrefab;


	void Awake()
	{
		characterData = new CharacterData();

		InitializeCharacter();
	}

	public CharacterController GetCharacterController()
	{
		return characterController;
	}

	public void InitializeCharacter()
	{
		// Cache components
		_attributes = GetComponent<Attributes>();
		_statsOffense = GetComponent<StatsOffense>();
		_statsDefense = GetComponent<StatsDefense>();
		_statsGeneral = GetComponent<StatsGeneral>();

		characterController = GetComponent<CharacterController>();

		LoadCharacterData();

		characterData.specialization.SetTotalSpecPoints(characterData.level);

		// Initialize Attributes
		_attributes.GenerateAugmentedCharacterAttributes(characterData.specialization, characterData.inventory);

		// Initialize statistics
		_statsOffense.GenerateAugmentedCharacterStatistics();
		_statsDefense.GenerateAugmentedCharacterStatistics();
		_statsGeneral.GenerateAugmentedCharacterStatistics();

		// Initialize Abilities

		// Set Abilities
		characterController.SetAbilities(basicAbilityPrefab, abilityQPrefab, abilityWPrefab, abilityEPrefab, abilityRPrefab);
	}

	public void UpdateCharacter()
	{
		LoadCharacterData();

		// Initialize Attributes
		_attributes.GenerateAugmentedCharacterAttributes(characterData.specialization, characterData.inventory);

		// Initialize statistics
		_statsOffense.GenerateAugmentedCharacterStatistics();
		_statsDefense.GenerateAugmentedCharacterStatistics();
		_statsGeneral.GenerateAugmentedCharacterStatistics();
	}

	public void AddItem(Item item)
	{
		characterData.inventory.Add(item);
		SaveCharacterData();
		UpdateCharacter();
	}

	public void RemoveItem(Item item)
	{
		characterData.inventory.Remove(item);
		SaveCharacterData();
		UpdateCharacter();
	}

	public void SaveCharacterData()
	{
		// Save the data to a binary file
		if (File.Exists(Application.persistentDataPath + "/" + name +".dat"))
		{
			//File.Delete(Application.persistentDataPath + "/PlayerData.gd");
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".dat", FileMode.Open);

			bf.Serialize(file, characterData);
			file.Close();
		}
		else
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/" + name + ".dat");
			bf.Serialize(file, characterData);
			file.Close();
		}
	}


	public void LoadCharacterData()
	{
		if (File.Exists(Application.persistentDataPath + "/" + name + ".dat"))
		{
			characterData.inventory.Clear();

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".dat", FileMode.Open);

			Debug.Log(file.Length.ToString());

			characterData = (CharacterData)bf.Deserialize(file);
			file.Close();

		}

		foreach (Item item in characterData.inventory)
		{
			Debug.Log(item.strength.ToString());
		}
	}




	[System.Serializable]
	public class CharacterData
	{
		public int level;
		public float experience;
		public ArrayList inventory;
		public Specialization specialization;

		public CharacterData()
		{
			level = 0;
			experience = 0;
			inventory = new ArrayList();
			specialization = new Specialization();
			specialization.SetTotalSpecPoints(level);
		}

		public void AddExperience(float xp)
		{
			experience += xp;

			while (experience >= 100)
			{
				level++;
				experience -= 100;
			}

			specialization.SetTotalSpecPoints(level);
		}
	}
}


[System.Serializable]
public class Specialization
{
	public int totalSpecPoints;
	public int spentSpecPoints;

	public int spec1;
	public int spec2;
	public int spec3;
	public int spec4;
	public int spec5;

	public Specialization()
	{
		totalSpecPoints = 0;
		spentSpecPoints = 0;
		spec1 = 0;
		spec2 = 0;
		spec3 = 0;
		spec4 = 0;
		spec5 = 0;
	}

	public void SetTotalSpecPoints(int level)
	{
		totalSpecPoints = level;
	}

	public bool AddPointToSpec(int spec)
	{
		if (spentSpecPoints < totalSpecPoints)
		{
			if (spec == 1)
			{
				spec1++;
				spentSpecPoints++;
				return true;
			}
			else if (spec == 2)
			{
				spec2++;
				spentSpecPoints++;
				return true;
			}
			else if (spec == 3)
			{
				spec3++;
				spentSpecPoints++;
				return true;
			}
			else if (spec == 4)
			{
				spec4++;
				spentSpecPoints++;
				return true;
			}
			else if (spec == 5)
			{
				spec5++;
				spentSpecPoints++;
				return true;
			}
		}
		return false;
	}

	public bool RemovePointFromSpec(int spec)
	{
		if (spentSpecPoints > 0)
		{
			if (spec == 1 && spec1 > 0)
			{
				spec1--;
				spentSpecPoints--;
				return true;
			}
			else if (spec == 2 && spec2 > 0)
			{
				spec2--;
				spentSpecPoints--;
				return true;
			}
			else if (spec == 3 && spec3 > 0)
			{
				spec3--;
				spentSpecPoints--;
				return true;
			}
			else if (spec == 4 && spec4 > 0)
			{
				spec4--;
				spentSpecPoints--;
				return true;
			}
			else if (spec == 5 && spec5 > 0)
			{
				spec5--;
				spentSpecPoints--;
				return true;
			}
		}
		return false;
	}
}
