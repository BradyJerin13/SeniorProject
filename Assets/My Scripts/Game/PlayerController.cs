using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	// The current group of characters being played
	public ArrayList characterGroup;
	//public CharacterManager[] characterGroup;

	public CharacterController focusCharacter;

	public float timedelay;

	void Awake()
	{
		characterGroup = new ArrayList();

		//characterGroup[0].enabled = true;
		//focusCharacter = characterGroup[0].GetCharacterController();
		//focusCharacter = characterGroup[0].GetCharacterController();//.GetComponent<CharacterController>();
		
	}

	// Use this for initialization
	void Start () 
	{
		//focusCharacter = (CharacterController)characterGroup[0];//.GetCharacterController();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (characterGroup.Count != 0)
		{
			MenuManager.menuManager.UpdateHealth(characterGroup);
		}

		//Moves the Player if the Left Mouse Button was clicked
		if (Input.GetMouseButtonDown(1))
		{
			RightClick();
		}
		else if (Input.GetMouseButton(1))// Hold to move
		{
			RightHold();
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			CharacterSwap(0);
			MenuManager.menuManager.RotateCharacterHUD(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			CharacterSwap(1);
			MenuManager.menuManager.RotateCharacterHUD(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			CharacterSwap(2);
			MenuManager.menuManager.RotateCharacterHUD(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			CharacterSwap(3);
			MenuManager.menuManager.RotateCharacterHUD(3);
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			CastAbility(0);
		}

		if (Input.GetKeyDown(KeyCode.W))
		{
			CastAbility(1);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			CastAbility(2);
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			CastAbility(3);
		}


		if (Input.GetKeyDown(KeyCode.Escape))
		{
			MenuManager.menuManager.ToggleLevelPanel();
		}
	}


	public void CharacterSwap(int index)
	{
		if(focusCharacter != null)
			focusCharacter.human = false;

		focusCharacter = (CharacterController)characterGroup[index];//.GetCharacterController();
		focusCharacter.human = true;
	}


	public void RightClick()
	{
		RaycastHit targetInfo = new RaycastHit();
		Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out targetInfo);

		if (targetInfo.collider != null)
		{
			focusCharacter.RightClick(targetInfo);
		}
		// Send command to Character Controller
	}

	public void RightHold()
	{
		RaycastHit targetInfo = new RaycastHit();
		Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out targetInfo);

		if (targetInfo.collider != null && Time.time > timedelay)
		{
			focusCharacter.RightClick(targetInfo);
			timedelay = Time.time + .25f;
		}
	}

	public void CastAbility(int index)
	{
		RaycastHit targetInfo = new RaycastHit();
		Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out targetInfo);

		// Send command to Character Controller
		if (index == 0)
		{
			focusCharacter.CastAbilityQ(targetInfo);
		}
		else if (index == 1)
		{
			focusCharacter.CastAbilityW(targetInfo);
		}
		else if (index == 2)
		{
			focusCharacter.CastAbilityE(targetInfo);
		}
		else if (index == 3)
		{
			focusCharacter.CastAbilityR(targetInfo);
		}
	}

	public void FillCharacterGroup(ArrayList charactergroup)
	{
		characterGroup = new ArrayList();

		// Set the Player Controller Character Group
		characterGroup = charactergroup;

		foreach (CharacterController character in characterGroup)
		{
			character.human = false;
			Debug.Log(character.ToString());
		}

		// Set the focus character to index 0
		focusCharacter = (CharacterController)characterGroup[0];//.GetCharacterController();
		focusCharacter.human = true;
	}

}
