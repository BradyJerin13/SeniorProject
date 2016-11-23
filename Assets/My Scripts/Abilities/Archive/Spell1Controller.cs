using UnityEngine;
using System.Collections;

public class Spell1Controller : MonoBehaviour
{
	public float lifetime;

	private float _spell1CoolDown = 1.0f;
	private float _spell1CoolDownTimer = 0.0f;
	public float speed;

	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		Destroy(gameObject, lifetime);
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time > _spell1CoolDownTimer)
		{
			_spell1CoolDownTimer = Time.time + _spell1CoolDown;
			gameObject.transform.localScale *= 1.5f;
		}
	}
}
