using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour
{
	// Base attributes for the character
	public float _baseStrength;
	public float _baseDexterity;
	public float _baseIntellect;
	public float _baseWill;
	public float _baseToughness;

	// Affected by items
	public float _augmentedStrength;
	public float _augmentedDexterity;
	public float _augmentedIntellect;
	public float _augmentedWill;
	public float _augmentedToughness;

	void Awake()
	{
		InitializeAttributes();
	}

	public void GenerateAugmentedCharacterAttributes(Specialization spec, ArrayList items)
	{
		_augmentedStrength = _baseStrength;
		_augmentedDexterity = _baseDexterity;
		_augmentedIntellect = _baseIntellect;
		_augmentedWill = _baseWill;
		_augmentedToughness = _baseToughness;

		_augmentedStrength += (float)spec.spec1;
		_augmentedDexterity += (float)spec.spec2;
		_augmentedIntellect += (float)spec.spec3;
		_augmentedWill += (float)spec.spec4;
		_augmentedToughness += (float)spec.spec5;

		if (items != null)
		{
			// Items
			foreach (Item item in items)
			{
				_augmentedStrength += item.strength;
				_augmentedDexterity += item.dexterity;
				_augmentedIntellect += item.intellect;
				_augmentedWill += item.will;
				_augmentedToughness += item.toughness;
			}
		}
	}

	public void InitializeEnemyAttributes(int difficulty)
	{
		_augmentedStrength = _baseStrength;
		_augmentedDexterity = _baseDexterity;
		_augmentedIntellect = _baseIntellect;
		_augmentedWill = _baseWill;
		_augmentedToughness = _baseToughness;

		_augmentedStrength += difficulty;
		_augmentedDexterity += difficulty;
		_augmentedIntellect += difficulty;
		_augmentedWill += difficulty;
		_augmentedToughness += difficulty;

	}

	public void InitializeAttributes()
	{
		_augmentedStrength = _baseStrength;
		_augmentedDexterity = _baseDexterity;
		_augmentedIntellect = _baseIntellect;
		_augmentedWill = _baseWill;
		_augmentedToughness = _baseToughness;
	}

	//public void InitializeAttributeData(AttributeData data)
	//{
	//	//if (attributeData == null)
	//	//{
	//	//	attributeData = new AttributeData();
	//	//}

	//	Debug.Log("str - " + data._baseStrength.ToString());

	//	attributeData._baseStrength = data._baseStrength;
	//	attributeData._baseDexterity = data._baseDexterity;
	//	attributeData._baseIntellect = data._baseIntellect;
	//	attributeData._baseWill = data._baseWill;
	//	attributeData._baseToughness = data._baseToughness;

	//	_baseStrength = attributeData._baseStrength;
	//	_baseDexterity = attributeData._baseDexterity;
	//	_baseIntellect = attributeData._baseIntellect;
	//	_baseWill = attributeData._baseWill;
	//	_baseToughness = attributeData._baseToughness;



	//	InitializeAttributes();
	//}
	


}

//public class AttributeData
//{
//	public float _baseStrength;
//	public float _baseDexterity;
//	public float _baseIntellect;
//	public float _baseWill;
//	public float _baseToughness;


//	public AttributeData()
//	{
//		_baseStrength = 0;
//		_baseDexterity = 0;
//		_baseIntellect = 0;
//		_baseWill = 0;
//		_baseToughness = 0;
//	}
//}