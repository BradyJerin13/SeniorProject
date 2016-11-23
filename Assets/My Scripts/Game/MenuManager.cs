using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public Canvas mainCanvasPrefab;
	public Canvas levelCanvasPrefab;
	public Canvas playerHUDPrefab;

	public Canvas mainCanvas;
	public Canvas levelCanvas;
	public ArrayList characterHUDs;

	public Canvas playerHUD;

	public GameObject attributePanel;
	public GameObject statisticsPanel;
	public GameObject itemsPanel;
	public GameObject inventoryPanel;
	public GameObject specializationPanel;
	public Text characterName;

	public static MenuManager menuManager;

	public CharacterManager characterManager;

	// Position to display the character in the Character Management page
	public Vector3 CharacterDisplayPosition;

	// The current selected item
	public Item selectedItem;

	// Menu representation of an item
	public GameObject buttonPrefab;

	// List of buttons that represent the items in the character and player inventory
	public ArrayList characterUIItems;
	public ArrayList playerUIInventory;
	public ArrayList storeUIBuyItems;
	public ArrayList storeUISellItems;
	public ArrayList storeUIBuyBackItems;

	public StoreManager store;
	public GameObject buyPanel;
	public GameObject sellPanel;
	public GameObject buybackPanel;

	public GameObject rewardsPanel;

	// Text for the Damage log in the level scene
	public RectTransform damageLogText;
	public RectTransform objectiveText;

	public InputField nametext;
	public Slider xpBar;

	#region

	#endregion



	/// <summary>
	/// Instantiate singleton.
	/// Instantiate the main menus
	/// </summary>
	void Awake()
	{
		characterUIItems = new ArrayList();
		playerUIInventory = new ArrayList();
		storeUIBuyItems = new ArrayList();
		storeUISellItems = new ArrayList();
		storeUIBuyBackItems = new ArrayList();
		characterHUDs = new ArrayList();

		if (menuManager == null)
		{
			DontDestroyOnLoad(gameObject);
			menuManager = this;
		}
		else if (menuManager != this)
		{
			Destroy(gameObject);
		}
		InstantiateMainMenu();
	}

	public void InstantiateMainMenu()
	{
		mainCanvas = (Canvas)Instantiate(mainCanvasPrefab, mainCanvasPrefab.transform.position, mainCanvasPrefab.transform.rotation);
	}

	


	public void DisplayMainPanel()
	{
		if (!mainCanvasPrefab.gameObject.activeSelf)
			mainCanvasPrefab.gameObject.SetActive(true);
	}

	public void ToggleLevelPanel()
	{
		MenuManager.menuManager.levelCanvas.gameObject.SetActive(!MenuManager.menuManager.levelCanvas.gameObject.activeSelf);
	}

	public void ExitApplication()
	{
		GameManager.gameManager.ExitApplication();
	}


	#region Play Scenario Page Methods

	public void InitializeScenarioPage()
	{ 
		
	}

	public void SetPlayerName()
	{
		Debug.Log(PlayerManager.playerManager.playerData.name);
		Debug.Log(nametext.text);
		PlayerManager.playerManager.playerData.name = nametext.text;
	}

	public void Post()
	{
		string text = "I am level " + PlayerManager.playerManager.playerData.level.ToString() + " in the game group.";

		Application.OpenURL("http://twitter.com/intent/tweet" +
					"?text=" + WWW.EscapeURL(text) +
					"&amp;lang=" + WWW.EscapeURL("en"));
	}


	#endregion




	#region Character Management Page Methods

	public void InitializeCharacterManagerPanels(GameObject panel)
	{
		foreach (RectTransform child in panel.transform)
		{
			if (child.name == "Panel : Specialization")
			{
				MenuManager.menuManager.specializationPanel = child.gameObject;
			}
			else if (child.name == "Text : Character Name")
			{
				MenuManager.menuManager.characterName = child.GetComponent<Text>();
			}
			else if (child.name == "XPBar")
			{
				MenuManager.menuManager.xpBar = child.GetComponent<Slider>();
			}
			else
			{
				foreach (RectTransform grandchild in child.transform)
				{
					if (grandchild.name == "Panel : Attributes")
					{
						MenuManager.menuManager.attributePanel = grandchild.gameObject;
					}
					else if (grandchild.name == "Panel : Statistics")
					{
						MenuManager.menuManager.statisticsPanel = grandchild.gameObject;
					}
					else if (grandchild.name == "Panel : Items")
					{
						MenuManager.menuManager.itemsPanel = grandchild.gameObject;
					}
					else if (grandchild.name == "Panel : Inventory")
					{
						MenuManager.menuManager.inventoryPanel = grandchild.gameObject;
					}
				}
			}
		}
	}

	public void InstantiateAllCharacters()
	{
		Vector3 instantiatePosition = new Vector3(-10.0f, -10.0f, -10.0f);

		foreach (CharacterManager character in PlayerManager.playerManager.characterPrefabs)
		{
			CharacterManager clone = (CharacterManager)Instantiate(character, instantiatePosition, character.transform.rotation);
			clone.LoadCharacterData();
			clone.GetComponent<CharacterController>().human = true;

			PlayerManager.playerManager.characterList.Add(clone);
		}
		PlayerManager.playerManager.SelectCharacter(0);
		PlayerManager.playerManager.UpdatePlayerLevel();
	}

	public void SelectCharacter(int i)
	{
		Vector3 oldPosition = new Vector3(-10.0f, -10.0f, -10.0f);
		PlayerManager.playerManager.selectedCharacter.transform.position = oldPosition;

		Vector3 instantiatePosition = new Vector3(0, .75f, 7f);
		PlayerManager.playerManager.SelectCharacter(i);
		PlayerManager.playerManager.selectedCharacter.transform.position = instantiatePosition;
	}

	public void UpdateAllCharacterManagementPanels()
	{
		MenuManager.menuManager.FillCharacterName();
		MenuManager.menuManager.FillCharacterExperienceBar();
		MenuManager.menuManager.FillCharacterAttributes();
		MenuManager.menuManager.FillCharacterStatistics();
		MenuManager.menuManager.FillCharacterSpecializationPanel();
		MenuManager.menuManager.FillCharacterItems();
		MenuManager.menuManager.FillPlayerInventory();
	}

	public void FillCharacterName()
	{
		MenuManager.menuManager.characterName.text = PlayerManager.playerManager.selectedCharacter.name.ToString();
	}

	public void FillCharacterExperienceBar()
	{
		MenuManager.menuManager.xpBar.value = PlayerManager.playerManager.selectedCharacter.characterData.experience / 100;
	}

	/// <summary>
	/// Fill the character attributes in the panel
	/// </summary>
	/// <param name="attrpanel">panel</param>
	public void FillCharacterAttributes()
	{
		MenuManager.menuManager.attributePanel.GetComponent<Text>().text = "";

		MenuManager.menuManager.attributePanel.GetComponent<Text>().text = "Level " + 
			PlayerManager.playerManager.selectedCharacter.characterData.level + "\n" +
			"Attributes\n" +
		PlayerManager.playerManager.selectedCharacter._attributes._augmentedStrength.ToString() + "\tStrength\n" +
		PlayerManager.playerManager.selectedCharacter._attributes._augmentedDexterity.ToString() + "\tDexterity\n" +
		PlayerManager.playerManager.selectedCharacter._attributes._augmentedIntellect.ToString() + "\tIntellect\n" +
		PlayerManager.playerManager.selectedCharacter._attributes._augmentedWill.ToString() + "\tWill\n" +
		PlayerManager.playerManager.selectedCharacter._attributes._augmentedToughness.ToString() + "\tToughness";
	}

	/// <summary>
	/// Fill the character stats in the panel
	/// </summary>
	/// <param name="statspanel">panel</param>
	public void FillCharacterStatistics()
	{
		MenuManager.menuManager.statisticsPanel.GetComponent<Text>().text = "";

		MenuManager.menuManager.statisticsPanel.GetComponent<Text>().text = "Statistics\n" +
		PlayerManager.playerManager.selectedCharacter._statsOffense._augmentedPhysicalDamage.ToString() + "\tPhysical Damage\n" +
		PlayerManager.playerManager.selectedCharacter._statsOffense._augmentedMagicDamage.ToString() + "\tMagical Damage\n" +
		PlayerManager.playerManager.selectedCharacter._statsOffense._augmentedAttackSpeed.ToString() + "\tAttack Speed\n" +
		PlayerManager.playerManager.selectedCharacter._statsOffense._augmentedCriticalChance.ToString() + "\tCritical Chance\n" +
		PlayerManager.playerManager.selectedCharacter._statsOffense._augmentedCriticalDamage.ToString() + "\tCritical Damage\n" +
		PlayerManager.playerManager.selectedCharacter._statsOffense._augmentedRange.ToString() + "\tRange\n" +

		PlayerManager.playerManager.selectedCharacter._statsDefense._augmentedLife.ToString() + "\tLife\n" +
		PlayerManager.playerManager.selectedCharacter._statsDefense._augmentedArmor.ToString() + "\tArmor\n" +
		PlayerManager.playerManager.selectedCharacter._statsDefense._augmentedMagicResist.ToString() + "\tMagic Resist\n" +

		PlayerManager.playerManager.selectedCharacter._statsGeneral._augmentedSpeed.ToString() + "\tSpeed\n" +
		PlayerManager.playerManager.selectedCharacter._statsGeneral._augmentedCoolDownReduction.ToString() + "\tCool Down Reduction\n";
	}


	/// <summary>
	/// Fill the character items in the panel
	/// </summary>
	/// <param name="itemsPanel"></param>
	public void FillCharacterItems()
	{
		foreach (GameObject g in menuManager.characterUIItems)
		{
			Destroy(g);
		}

		menuManager.characterUIItems.Clear();

		Vector2 btnplacementpoint = new Vector2(0, 0);

		float width = buttonPrefab.GetComponent<RectTransform>().rect.width;
		float height = buttonPrefab.GetComponent<RectTransform>().rect.height;
		float panelheight = 0;

		btnplacementpoint.x = width / 2;
		btnplacementpoint.y = height / 2 * -1;

		foreach (Item item in PlayerManager.playerManager.selectedCharacter.characterData.inventory)
		{
			GameObject btnclone = (GameObject)Instantiate(buttonPrefab);
			Button button = btnclone.GetComponent<Button>();

			FillItemButton(item, btnclone);

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => { SelectItem(item); });
			button.onClick.AddListener(() => { PlayerManager.playerManager.selectedCharacter.RemoveItem(item); });
			button.onClick.AddListener(() => { PlayerManager.playerManager.AddItem(item); });
			button.onClick.AddListener(() => { MenuManager.menuManager.UpdateAllCharacterManagementPanels(); });
			button.onClick.AddListener(() => { Destroy(button); });

			btnclone.transform.SetParent(MenuManager.menuManager.itemsPanel.transform, false);
			
			btnclone.GetComponent<RectTransform>().anchoredPosition = btnplacementpoint;

			menuManager.characterUIItems.Add(btnclone);

			btnplacementpoint.y -= height;
			panelheight += height;
		}
		MenuManager.menuManager.itemsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(130, panelheight);
	}

	public void FillItemButton(Item item, GameObject btn)
	{
		foreach (Transform child in btn.transform)
		{
			if (child.name == "Text : Name")
			{
				child.GetComponent<Text>().text = item.name.ToString();
			}
			else if (child.name == "Text : Level")
			{
				child.GetComponent<Text>().text = "Lvl" + item.level.ToString();
			}
			else if (child.name == "Text : Cost")
			{
				child.GetComponent<Text>().text = item.cost.ToString() + "c";

			}
			else if (child.name == "Text : Attributes")
			{
				child.GetComponent<Text>().text = "str" + item.strength.ToString() + "\n"
												+ "dex" + item.dexterity.ToString() + "\n"
												+ "int" + item.intellect.ToString() + "\n"
												+ "wil" + item.will.ToString() + "\n"
												+ "tuf" + item.toughness.ToString();
			}
		}
	}

	public void FillPlayerInventory()
	{
		foreach (GameObject g in menuManager.playerUIInventory)
		{
			Destroy(g);
		}

		menuManager.playerUIInventory.Clear();

		Vector2 btnplacementpoint = new Vector2(0, 0);

		float width = buttonPrefab.GetComponent<RectTransform>().rect.width;
		float height = buttonPrefab.GetComponent<RectTransform>().rect.height;
		float panelheight = 0;

		btnplacementpoint.x = width / 2;
		btnplacementpoint.y = height / 2 * -1;

		foreach (Item item in PlayerManager.playerManager.playerData.inventory)
		{
			GameObject btnclone = (GameObject)Instantiate(buttonPrefab);

			Button button = btnclone.GetComponent<Button>();

			FillItemButton(item, btnclone);

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => { MenuManager.menuManager.SelectItem(item); });
			button.onClick.AddListener(() => { PlayerManager.playerManager.RemoveItem(item); });
			button.onClick.AddListener(() => { PlayerManager.playerManager.selectedCharacter.AddItem(item); });
			button.onClick.AddListener(() => { MenuManager.menuManager.UpdateAllCharacterManagementPanels(); });
			button.onClick.AddListener(() => { Destroy(btnclone); });

			btnclone.transform.SetParent(MenuManager.menuManager.inventoryPanel.transform, false);
			
			btnclone.GetComponent<RectTransform>().anchoredPosition = btnplacementpoint;
			
			menuManager.playerUIInventory.Add(btnclone);

			btnplacementpoint.y -= height;
			panelheight += height;
		}
		MenuManager.menuManager.inventoryPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(130, panelheight);
	}

	public void FillCharacterSpecializationPanel()
	{
		foreach (Transform child in MenuManager.menuManager.specializationPanel.transform)
		{
			if (child.name.Contains("Strength"))
			{
				child.GetComponent<Text>().text = "Str +" + PlayerManager.playerManager.selectedCharacter.characterData.specialization.spec1.ToString();
			}
			else if (child.name.Contains("Dexterity"))
			{
				child.GetComponent<Text>().text = "Dex +" + PlayerManager.playerManager.selectedCharacter.characterData.specialization.spec2.ToString();
			}
			else if (child.name.Contains("Intellect"))
			{
				child.GetComponent<Text>().text = "Int +" + PlayerManager.playerManager.selectedCharacter.characterData.specialization.spec3.ToString();
			}
			else if (child.name.Contains("Will"))
			{
				child.GetComponent<Text>().text = "Wil +" + PlayerManager.playerManager.selectedCharacter.characterData.specialization.spec4.ToString();
			}
			else if (child.name.Contains("Toughness"))
			{
				child.GetComponent<Text>().text = "Tuf +" + PlayerManager.playerManager.selectedCharacter.characterData.specialization.spec5.ToString();
			}
			else if (child.name.Contains("Points"))
			{
				child.GetComponent<Text>().text = PlayerManager.playerManager.selectedCharacter.characterData.specialization.spentSpecPoints.ToString() + "/" + PlayerManager.playerManager.selectedCharacter.characterData.specialization.totalSpecPoints.ToString();
			}
		}
	}

	public void AddSpecPoint(int index)
	{
		PlayerManager.playerManager.selectedCharacter.characterData.specialization.AddPointToSpec(index);
		PlayerManager.playerManager.selectedCharacter.SaveCharacterData();
		PlayerManager.playerManager.selectedCharacter.UpdateCharacter();
		MenuManager.menuManager.UpdateAllCharacterManagementPanels();
	}

	public void RemoveSpecPoint(int index)
	{
		PlayerManager.playerManager.selectedCharacter.characterData.specialization.RemovePointFromSpec(index);
		PlayerManager.playerManager.selectedCharacter.SaveCharacterData();
		PlayerManager.playerManager.selectedCharacter.UpdateCharacter();
		MenuManager.menuManager.UpdateAllCharacterManagementPanels();
	}


	#endregion


	#region Store GUI


	public void InitializeStorePanels(GameObject panel)
	{
		foreach (RectTransform child in panel.transform)
		{
			foreach (RectTransform grandchild in child.transform)
			{
				if (grandchild.name == "Panel : Buy")
				{
					MenuManager.menuManager.buyPanel = grandchild.gameObject;
				}
				else if (grandchild.name == "Panel : Sell")
				{
					MenuManager.menuManager.sellPanel = grandchild.gameObject;
				}
				else if (grandchild.name == "Panel : BuyBack")
				{
					MenuManager.menuManager.buybackPanel = grandchild.gameObject;
				}
			}
		}

		MenuManager.menuManager.store.InitializeStore();
	}


	public void FillStorePanels(ArrayList buyitems, ArrayList buybackitems)
	{
		Vector2 btnplacementpoint = new Vector2(0, 0);

		// Buy Panel
		foreach (GameObject g in menuManager.storeUIBuyItems)
		{
			Destroy(g);
		}

		menuManager.storeUIBuyItems.Clear();

		float width = buttonPrefab.GetComponent<RectTransform>().rect.width;
		float height = buttonPrefab.GetComponent<RectTransform>().rect.height;
		float panelheight = 0;

		btnplacementpoint.x = width / 2;
		btnplacementpoint.y = height / 2 * -1;

		foreach (Item item in buyitems)
		{
			GameObject btnclone = (GameObject)Instantiate(buttonPrefab);

			Button button = btnclone.GetComponent<Button>();
			FillItemButton(item, btnclone);

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => { MenuManager.menuManager.SelectItem(item); });
			button.onClick.AddListener(() => { PlayerManager.playerManager.RemoveCurrency(item.cost); });
			button.onClick.AddListener(() => { menuManager.store.RemoveItem(item); });
			button.onClick.AddListener(() => { PlayerManager.playerManager.AddItem(item); });
			button.onClick.AddListener(() => { menuManager.store.UpdateStore(); });
			button.onClick.AddListener(() => { Destroy(btnclone); });

			btnclone.transform.SetParent(MenuManager.menuManager.buyPanel.transform, false);
			btnclone.GetComponent<RectTransform>().anchoredPosition = btnplacementpoint;

			menuManager.storeUIBuyItems.Add(btnclone);
			btnplacementpoint.y -= height;
			panelheight += height;
		}
		MenuManager.menuManager.buyPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(130, panelheight);



		// Sell Panel
		foreach (GameObject g in menuManager.storeUISellItems)
		{
			Destroy(g);
		}

		menuManager.storeUISellItems.Clear();

		btnplacementpoint.x = width / 2;
		btnplacementpoint.y = height / 2 * -1;
		panelheight = 0;
		
		foreach (Item item in PlayerManager.playerManager.playerData.inventory)
		{
			GameObject btnclone = (GameObject)Instantiate(buttonPrefab);

			Button button = btnclone.GetComponent<Button>();
			FillItemButton(item, btnclone);

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => { MenuManager.menuManager.SelectItem(item); });
			button.onClick.AddListener(() => { PlayerManager.playerManager.RemoveItem(item); });
			button.onClick.AddListener(() => { menuManager.store.AddItem(item); });
			button.onClick.AddListener(() => { PlayerManager.playerManager.AddCurrency(item.cost); });
			button.onClick.AddListener(() => { menuManager.store.UpdateStore(); });
			button.onClick.AddListener(() => { Destroy(btnclone); });

			btnclone.transform.SetParent(MenuManager.menuManager.sellPanel.transform, false);

			btnclone.GetComponent<RectTransform>().anchoredPosition = btnplacementpoint;

			menuManager.storeUISellItems.Add(btnclone);

			btnplacementpoint.y -= height;
			panelheight += height;
		}
		MenuManager.menuManager.sellPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(130, panelheight);


		// Buyback Panel
		foreach (GameObject g in menuManager.storeUIBuyBackItems)
		{
			Destroy(g);
		}

		menuManager.storeUIBuyBackItems.Clear();

		if (buybackitems != null)
		{
			btnplacementpoint.x = width / 2;
			btnplacementpoint.y = height / 2 * -1;
			panelheight = 0;

			foreach (Item item in buybackitems)
			{
				GameObject btnclone = (GameObject)Instantiate(buttonPrefab);

				Button button = btnclone.GetComponent<Button>();
				FillItemButton(item, btnclone);

				button.onClick.RemoveAllListeners();
				button.onClick.AddListener(() => { MenuManager.menuManager.SelectItem(item); });
				button.onClick.AddListener(() => { PlayerManager.playerManager.RemoveCurrency(item.cost); });
				button.onClick.AddListener(() => { menuManager.store.BuyBackItem(item); });
				button.onClick.AddListener(() => { PlayerManager.playerManager.AddItem(item); });
				button.onClick.AddListener(() => { menuManager.store.UpdateStore(); });
				button.onClick.AddListener(() => { Destroy(btnclone); });

				btnclone.transform.SetParent(MenuManager.menuManager.buybackPanel.transform, false);

				btnclone.GetComponent<RectTransform>().anchoredPosition = btnplacementpoint;

				menuManager.storeUIBuyBackItems.Add(btnclone);

				btnplacementpoint.y -= height;
				panelheight += height;
			}
		}
		MenuManager.menuManager.buybackPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(130, panelheight);
		
	}

	#endregion




	#region In-Level Gui

	public void InstantiateLevelMenu()
	{
		MenuManager.menuManager.characterHUDs.Clear();

		levelCanvas = (Canvas)Instantiate(levelCanvasPrefab, levelCanvasPrefab.transform.position, levelCanvasPrefab.transform.rotation);
		playerHUD = (Canvas)Instantiate(playerHUDPrefab, playerHUDPrefab.transform.position, playerHUDPrefab.transform.rotation);

		foreach (Transform child in playerHUD.transform)
		{
			if (child.name == "Panel : Objectives")
			{
				objectiveText = child.GetComponent<RectTransform>();
				objectiveText.GetComponentInChildren<Text>().text = "";
			}
			else if (child.name == "Panel : DamageLog")
			{
				damageLogText = child.GetComponent<RectTransform>();
				damageLogText.GetComponentInChildren<Text>().text = "";
			}
			else if (child.name == "Panel : Rewards")
			{
				rewardsPanel = child.gameObject;
			}
			else if (child.name.Contains("Panel"))
			{
				MenuManager.menuManager.characterHUDs.Add(child.GetComponent<RectTransform>());
			}
		}

		int index = 0;
		foreach (RectTransform panel in MenuManager.menuManager.characterHUDs)
		{
			Debug.Log("HUDS" + panel);
			foreach (Transform child in panel.transform)
			{
				if (child.name == "Q")
				{

				}
				else if (child.name == "W")
				{

				}
				else if (child.name == "E")
				{

				}
				else if (child.name == "R")
				{

				}
				else if (child.name == "Index")
				{
					child.GetComponent<Text>().text = (index + 1).ToString();
				}
				else if (child.name == "Portrait")
				{

				}
				else if (child.name == "CharacterName")
				{
					CharacterController character = (CharacterController)PlayerManager.playerManager.playerController.characterGroup[index];
					child.GetComponent<Text>().text = character.name.ToString().Replace("(Clone)", "");
				}
				else if (child.name == "LifeBar")
				{
					child.GetComponent<Slider>().value = 1.0f;
				}
				
			}
			++index;
		}
		RotateCharacterHUD(0);
		UpdateObjectives();
	}

	public void FillRewardsPanel(float experience, float currency, ArrayList items)
	{
		foreach (RectTransform child in rewardsPanel.transform)
		{
			if (child.name == "Text : ExperienceReceived")
			{
				child.GetComponent<Text>().text = "Experience Received : " + experience.ToString() + "xp";
			}
			else if (child.name == "Text : CurrencyReceived")
			{
				child.GetComponent<Text>().text = "Currency Received : " + currency.ToString() + "c";
			}
			else if (child.name == "ScrollView : ItemsReceived")
			{
				Vector2 btnplacementpoint = new Vector2(0, 0);

				float width = buttonPrefab.GetComponent<RectTransform>().rect.width;
				float height = buttonPrefab.GetComponent<RectTransform>().rect.height;
				float panelheight = 0;

				btnplacementpoint.x = width / 2;
				btnplacementpoint.y = height / 2 * -1;

				foreach (RectTransform grandchild in child.transform)
				{
					if (grandchild.name == "Panel : ItemsReceived")
					{
						foreach (Item item in items)
						{
							GameObject btnclone = (GameObject)Instantiate(buttonPrefab);

							Button button = btnclone.GetComponent<Button>();

							FillItemButton(item, btnclone);

							btnclone.transform.SetParent(grandchild.transform, false);

							btnclone.GetComponent<RectTransform>().anchoredPosition = btnplacementpoint;

							menuManager.storeUIBuyBackItems.Add(btnclone);

							btnplacementpoint.y -= height;
							panelheight += height;
						}
						grandchild.GetComponent<RectTransform>().sizeDelta = new Vector2(130, panelheight);
					}
				}

			}
		}

		rewardsPanel.SetActive(true);
	}


	/// <summary>
	/// Rotates the character hud to the index of the focused character
	/// </summary>
	/// <param name="index"></param>
	public void RotateCharacterHUD(int index)
	{
		if (index == 0)
		{
			RotateCharacterHUD(index, new Vector3(0, 0, 0));
		}
		else if (index == 1)
		{
			RotateCharacterHUD(index, new Vector3(-125, 0, 0));
		}
		else if (index == 2)
		{
			RotateCharacterHUD(index, new Vector3(-200, 0, 0));
		}
		else if (index == 3)
		{
			RotateCharacterHUD(index, new Vector3(-300, 0, 0));
		}
	}

	/// <summary>
	/// Math for rotating and scaling the character huds
	/// </summary>
	/// <param name="index"></param>
	/// <param name="position"></param>
	private void RotateCharacterHUD(int index, Vector3 position)
	{
		for (int i = 0; i < 4; ++i)
		{
			RectTransform temp = (RectTransform)characterHUDs[i];

			if (i == index - 1)
			{
				position.y = 50;
				temp.localScale = new Vector3(.66f, .66f, .66f);
				temp.anchoredPosition = position;
				position.x += 125;
			}
			else if (i == index + 1)
			{
				position.y = 50;
				temp.localScale = new Vector3(.66f, .66f, .66f);
				temp.anchoredPosition = position;
				position.x += 100;
			}
			else if (i == index)
			{
				position.y = 75;
				temp.localScale = new Vector3(1, 1, 1);
				temp.anchoredPosition = position;
				position.x += 125;
			}
			else
			{
				position.y = 50;
				temp.localScale = new Vector3(.66f, .66f, .66f);
				temp.anchoredPosition = position;
				position.x += 100;
			}
		}
	}

	// Update the character hud health
	public void UpdateHealth(ArrayList characters)
	{
		RectTransform temp;
		CharacterController tempCharacter;

		for (int i = 0; i < 4; ++i)
		{
			temp = (RectTransform)characterHUDs[i];
			tempCharacter = (CharacterController)characters[i];

			if (temp != null && tempCharacter != null)
				temp.GetComponentInChildren<Slider>().value = tempCharacter._statsDefense._combatLife / tempCharacter._statsDefense._augmentedLife;
		}
	}

	// Update the character hud health
	public void UpdateObjectives()
	{
		string objectivestring = "Enemy kills: " + LevelManager.levelManager.EnemyKillCount.ToString()
								+ "/" + LevelManager.levelManager.EnemyKillTotal.ToString() + "\n"
								//+ "Boss kills: " + LevelManager.levelManager.BossKillCount.ToString()
								//+ "/" + LevelManager.levelManager.BossKillTotal.ToString() + "\n"
								+ "Character Deaths: " +LevelManager.levelManager.CharacterDeathCount.ToString();

		objectiveText.GetComponentInChildren<Text>().text = objectivestring;
	}

	// Displays the damage text in the combat log
	public void DamageText(float damage, string name)
	{
		Debug.Log(damage.ToString());
		string newname = name.Replace("(Clone)", "");

		damageLogText.GetComponentInChildren<Text>().text = (damage.ToString() + " to " + newname + "\n" + (damageLogText.GetComponentInChildren<Text>().text));
	}

	#endregion

	#region Menutools
	public void TogglePanel(GameObject panel)
	{
		panel.SetActive(!panel.activeSelf);
	}

	#endregion

	public void SelectItem(Item item)
	{
		Debug.Log("ITEM SELECTED");
		selectedItem = item;
	}
}
