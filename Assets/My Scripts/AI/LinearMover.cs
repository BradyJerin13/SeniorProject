using UnityEngine;
using System.Collections;

public class LinearMover : MonoBehaviour
{
	public float speed;
	public float lifeTime;

	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.up * speed;
		Destroy(gameObject, lifeTime);
	}
}
