using UnityEngine;
using System.Collections;

public class EndPortalController : InteractableController
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public override void Interact(GameObject gameObject)
	{
		LevelManager.levelManager.LevelComplete();
	}
}
