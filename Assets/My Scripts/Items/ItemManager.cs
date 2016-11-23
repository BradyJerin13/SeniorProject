//using UnityEngine;
//using System.Collections;

//public class ItemManager : MonoBehaviour
//{
//}
//	public ItemData itemData;

//	public Attributes _attributes;
//	public StatsOffense _statsOffense;
//	public StatsDefense _statsDefense;
//	public StatsGeneral _statsGeneral;

//	void Awake()
//	{
//		itemData = new ItemData();

//		InitializeItem();
//	}

//	public void InitializeItem()
//	{
//		_attributes = GetComponent<Attributes>();
//		_statsOffense = GetComponent<StatsOffense>();
//		_statsDefense = GetComponent<StatsDefense>();
//		_statsGeneral = GetComponent<StatsGeneral>();

//		itemData.attributeData = _attributes.attributeData;


//		itemData.attributeData._baseStrength = _attributes.attributeData._baseStrength;
//		//Debug.Log(attributeData._baseStrength);
//		itemData.attributeData._baseDexterity = _attributes.attributeData._baseDexterity;
//		itemData.attributeData._baseIntellect = _attributes.attributeData._baseIntellect;
//		itemData.attributeData._baseWill = _attributes.attributeData._baseWill;
//		itemData.attributeData._baseToughness = _attributes.attributeData._baseToughness;



//		Debug.Log(_attributes.attributeData._baseStrength);
//		Debug.Log(itemData.attributeData._baseStrength);

//		_attributes.InitializeAttributes();
//		_statsOffense.InitializeStats();
//		_statsDefense.InitializeStats();
//		_statsGeneral.InitializeStats();
//	}

//	public Attributes GetAttributes()
//	{
//		return _attributes;
//	}

//	public StatsOffense GetStatsOffense()
//	{
//		return _statsOffense;
//	}

//	public StatsDefense GetStatsDefense()
//	{
//		return _statsDefense;
//	}

//	public StatsGeneral GetStatsGeneral()
//	{
//		return _statsGeneral;
//	}

//	public void InitializeItemData(ItemData data)
//	{
//		Debug.Log(data.attributeData._baseStrength);
//		itemData = data;
//		Debug.Log(itemData.attributeData._baseStrength);
//		_attributes.InitializeAttributeData(itemData.attributeData);
//		//InitializeItem();
//	}

//}
//[System.Serializable]
//public class ItemData
//{
//	public AttributeData attributeData;
	

//	public ItemData()
//	{
//		attributeData = new AttributeData();
//	}
//}