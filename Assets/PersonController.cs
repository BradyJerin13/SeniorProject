using UnityEngine;
using System.Collections;

public class PersonController : InteractableController
{
	public void Update()
	{
		if (targetObject != null)
		{
			float distanceFromTarget = Vector3.Distance(thisTransform.position, targetObject.transform.position);
			if (distanceFromTarget <= 2)
			{
			
			}
			else if (distanceFromTarget > 2)
			{
				thisTransform.position = Vector3.MoveTowards(thisTransform.position, targetObject.transform.position, 5 * Time.deltaTime);
			}
		}
	}

	public override void Interact(GameObject gameObject)
	{
		targetObject = gameObject;

		//distanceFromTarget = Vector3.Distance(thisTransform.position, gameObject.transform.position);
	//	if (distanceFromTarget < 10)
		//{
		//}

		//foreach (GameObject drop in drops)
		//{
		//	GameObject clone = (GameObject)GameObject.Instantiate(drop, new Vector3(base.thisTransform.position.x, thisTransform.position.y + 5, thisTransform.position.z), drop.transform.rotation);
		//	DropController controller = clone.GetComponent<DropController>();
		//	controller.SetTarget(gameObject);
		//}
	}

}
