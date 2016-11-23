using UnityEngine;
using System.Collections;

public class BasicAttackController : MonoBehaviour {

	// Use this for initialization
	public float lifetime;
	public float speed;

	void Start()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		Destroy(gameObject, lifetime);
	}

	void OnTriggerEnter(Collider other)
	{
		if(!other.CompareTag("Hero"))
		{
			Destroy(gameObject);
		}
	}
}
