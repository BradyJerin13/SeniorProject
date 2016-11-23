using UnityEngine;
using System.Collections;

public class AOESpellController : MonoBehaviour
{
	// Use this for initialization
	public float lifetime;

	void Start()
	{
		Destroy(gameObject, lifetime);
	}
}
