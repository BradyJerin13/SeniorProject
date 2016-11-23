using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour
{
	

	//public ItemManager selectedItem;
	//public ItemManager[] buyableItems;
	//public ItemManager[] buybackableItems;

	//public PlayerManager playerManager;
	//public ItemManager[] playerItems;

	public ArrayList buyItems;
	public ArrayList buybackItems;


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}


	public void InitializeStore()
	{
		//buyItems.Clear();
		buyItems = new ArrayList();
		buybackItems = new ArrayList();

		// Randomly Generate Items
		for (int i = 0; i < 5; ++i)
		{
			Item item = new Item(2);
			buyItems.Add(item);
		}


		// Load the buyback data

		// Fill the store panels
		MenuManager.menuManager.FillStorePanels(buyItems, buybackItems);
	}

	public void UpdateStore()
	{
		MenuManager.menuManager.FillStorePanels(buyItems, buybackItems);
	}


	public void RemoveItem(Item item)
	{
		buyItems.Remove(item);
		// Find Item to buy
		// Get currency from player
		// Give item to player
	}

	public void AddItem(Item item)
	{
		buybackItems.Add(item);
		// Find Item to sell
		// Get currency from store 
		// Give currency from store
		// Remove item from player
	}

	public void BuyBackItem(Item item)
	{
		buybackItems.Remove(item);
	}
}
