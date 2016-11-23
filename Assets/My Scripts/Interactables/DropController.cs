using UnityEngine;
using System.Collections;

public class DropController : MonoBehaviour
{
	private Transform thisTransform;
	public GameObject targetObject;

	// Use this for initialization
	void Start()
	{
		thisTransform = transform;
	}

	// Update is called once per frame
	void Update()
	{
		thisTransform.position = Vector3.MoveTowards(thisTransform.position, targetObject.transform.position, 2 * Time.deltaTime);
	
		if(Vector3.Distance(thisTransform.position, targetObject.transform.position) == 0)
		{
			Destroy(gameObject);
		}
	}

	public void SetTarget(GameObject gameobject)
	{
		targetObject = gameobject;
	}
}
