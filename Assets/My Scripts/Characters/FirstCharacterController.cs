using UnityEngine;
using System.Collections;

public class FirstCharacterController : CharacterController
{

	//public override void Awake()
	//{
	//	base.Awake();
	//}

	//public override void Start()
	//{
	//	base.Start();
	//}

	//public override void Update()
	//{
	//	base.Update();
	//}

	//public override void Move(Vector3 targetpoint)
	//{
	//	targetPoint = targetpoint;
	//	Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
	//	thisTransform.rotation = targetRotation;
	//}

	//public override void CastAbilityQ(GameObject target, Vector3 point)
	//{
	//	targetObject = target;
	//	Debug.Log("heal");

	//	if (target.tag == "Friendly")
	//	{
	//		Debug.Log("heal");
	//		Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
	//		thisTransform.rotation = targetRotation;

	//		GameObject clone;
	//		clone = (GameObject)Instantiate(abilityQPrefab, thisTransform.position, thisTransform.rotation);

	//		abilityQController = clone.GetComponent<AbilityController>();

	//		abilityQController.SetTargetObject(target);
	//		abilityQController.GenerateCombatStatistics(_statsGeneral, _statsOffense, _statsDefense);
	//	}
	//}

	//public override void SetAbilities(GameObject basic, GameObject Q, GameObject W, GameObject E, GameObject R)
	//{
	//	basicAbilityPrefab = basic;
	//	abilityQPrefab = Q;
	//	abilityWPrefab = W;
	//	abilityEPrefab = E;
	//	abilityRPrefab = R;
	//}
}
