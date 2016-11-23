using UnityEngine;
using System.Collections;

public class EnemyAttributeController : MonoBehaviour 
{
	public float maximumHealth;
	public float _currentHealth;

	// Use this for initialization
	void Start () 
	{
		_currentHealth = maximumHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_currentHealth <= 0.0f)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("BasicAttack"))
		{
			_currentHealth -= 1;
		}
		else if (other.CompareTag("Spell1"))
		{
			_currentHealth -= 3;
		}
		else if (other.CompareTag("Spell3"))
		{
			_currentHealth -= 5;
		}
		else if(other.CompareTag("EnemyHeal"))
		{
			_currentHealth += 5;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Spell2"))
		{
			_currentHealth = _currentHealth * .99f;
		}
	}
}
