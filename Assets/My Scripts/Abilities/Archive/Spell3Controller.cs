using UnityEngine;
using System.Collections;

public class Spell3Controller : MonoBehaviour 
{
	public float lifetime;

	private float _spell3CoolDown = 1.0f;
	private float _spell3CoolDownTimer = 0.0f;

	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Time.time > _spell3CoolDownTimer)
		{
			_spell3CoolDownTimer = Time.time + _spell3CoolDown;
			gameObject.transform.localScale *= 3;
		}
	}
}
