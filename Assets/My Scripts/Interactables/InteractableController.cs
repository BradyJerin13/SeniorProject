using UnityEngine;
using System.Collections;

public class InteractableController : MonoBehaviour
{
	public GameObject[] drops;

	public Transform thisTransform;

	public GameObject targetObject;
	public float distanceFromTarget;

	void Awake()
	{
		thisTransform = transform;
	}

	public virtual void Interact(GameObject gameObject)
	{
		foreach (GameObject drop in drops)
		{
			GameObject clone = (GameObject)GameObject.Instantiate(drop, new Vector3(thisTransform.position.x, thisTransform.position.y + 5, thisTransform.position.z), drop.transform.rotation);
			DropController controller = clone.GetComponent<DropController>();
			controller.SetTarget(gameObject);
		}
	}
}
