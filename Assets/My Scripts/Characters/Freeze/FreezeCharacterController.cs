using UnityEngine;
using System.Collections;

public class FreezeCharacterController : CharacterController
{
	public GameObject[] targets;



#region Abilities
	//public override void CastBasicAbility(GameObject targetobject)
	//{
	//	// Global cooldown check
	//	if (!_statsGeneral.CheckCooldown())
	//	{
	//		// Target Check
	//		if (targetobject.tag == "Enemy")
	//		{
	//			// Set target
	//			targetObject = targetobject;

	//			// Range Check
	//			if (Vector3.Distance(thisTransform.position, targetObject.transform.position) <= _statsOffense._combatRange)
	//			{
	//				StopAllCoroutines();
	//				StartCoroutine(CastAbilityBasicCoroutine());
	//			}
	//		}
	//	}
	//}
	//public override IEnumerator CastAbilityBasicCoroutine()
	//{
	//	while (targetObject != null)
	//	{
	//		if (!_statsGeneral.CheckCooldown())
	//		{
	//			StatsGeneral tempstats = basicAbilityPrefab.GetComponent<StatsGeneral>();

	//			//Rotate to target
	//			Quaternion targetRotation = Quaternion.LookRotation(targetObject.transform.position - transform.position);
	//			targetRotation.x = 0;
	//			targetRotation.z = 0;
	//			thisTransform.rotation = targetRotation;

	//			Debug.Log("PEW");

	//			// Cast ability
	//			GameObject clone = (GameObject)Instantiate(basicAbilityPrefab, thisTransform.position, thisTransform.rotation);

	//			cachedAbilityBasicController = clone.GetComponent<AbilityController>();

	//			// Set target and stats
	//			cachedAbilityBasicController.SetTargetObject(targetObject);
	//			cachedAbilityBasicController.GenerateCombatStatistics(_statsGeneral, _statsOffense, _statsDefense);

	//			// Start global cooldown
	//			_statsGeneral.StartGlobalCooldown(_statsOffense._combatAttackSpeed);
	//		}
	//		yield return null;
	//	}
	//}


	//public override void CastAbilityQ(RaycastHit targetinfo)
	//{
	//	// Global cooldown check
	//	if (!_statsGeneral.CheckCooldown())
	//	{
	//		// Set target
	//		targetPoint = targetinfo.point;

	//		StatsGeneral tempstats = abilityQPrefab.GetComponent<StatsGeneral>();

	//		// Ability Cooldown Check and start
	//		if (!tempstats.CheckCooldown())
	//		{
	//			StopAllCoroutines();
	//			StartCoroutine(CastAbilityQCoroutine(targetinfo));
	//		}
	//	}
	//}

	//public override IEnumerator CastAbilityQCoroutine(RaycastHit targetinfo)
	//{
	//	// Rotate
	//	Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
	//	targetRotation.x = 0;
	//	targetRotation.z = 0;
	//	thisTransform.rotation = targetRotation;

	//	// Cast ability
	//	GameObject clone = (GameObject)Instantiate(abilityQPrefab, thisTransform.position, thisTransform.rotation);

	//	cachedAbilityQController = clone.GetComponent<AbilityController>();

	//	// Set target and stats
	//	cachedAbilityQController.GenerateCombatStatistics(_statsGeneral, _statsOffense, _statsDefense);

	//	// Start global cooldown
	//	_statsGeneral.StartCooldown();

	//	yield return null;
	//}


	/// <summary>
	/// Target Heal Ability
	/// 
	/// This is just a sample method for casting an ability
	/// </summary>
	/// <param name="targetinfo"></param>
	public override void CastAbilityW(RaycastHit targetinfo)
	{
		// Global cooldown check
		if (!_statsGeneral.CheckCooldown())
		{
			StatsGeneral tempstats = abilityWPrefab.GetComponent<StatsGeneral>();

			// Ability Cooldown Check and start
			if (!tempstats.CheckCooldown())
			{
				StopAllCoroutines();
				StartCoroutine(CastAbilityWCoroutine());
			}
		}
	}

	public override IEnumerator CastAbilityWCoroutine()
	{
		// Cast ability
		GameObject clone = (GameObject)Instantiate(abilityWPrefab, thisTransform.transform.position, thisTransform.rotation);

		cachedAbilityWController = clone.GetComponent<AbilityController>();

		// Set target and stats
		cachedAbilityWController.GenerateCombatStatistics(_statsGeneral, _statsOffense, _statsDefense);

		// Start global cooldown
		_statsGeneral.StartCooldown();

		yield return null;
	}

	public override void CastAbilityE(RaycastHit targetinfo)
	{
		// Global cooldown check
		if (!_statsGeneral.CheckCooldown())
		{
			// Set target
			targetPoint = targetinfo.point;

			targetPoint.y = 1;
			
			// Range Check
			if (targetDistance <= abilityEController.GetRange())
			{
				StatsGeneral tempstats = abilityEPrefab.GetComponent<StatsGeneral>();

				// Ability Cooldown Check and start
				if (!tempstats.CheckCooldown())
				{
					StopAllCoroutines();
					StartCoroutine(CastAbilityECoroutine());
				}
			}
		}
	}

	public override IEnumerator CastAbilityECoroutine()
	{
		// Rotate
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
		targetRotation.x = 0;
		targetRotation.z = 0;
		thisTransform.rotation = targetRotation;

		// Cast ability
		thisTransform.position = targetPoint;

		// Start global cooldown
		_statsGeneral.StartCooldown();

		yield return null;
	}


	//public override void CastAbilityR(RaycastHit targetinfo)
	//{
	//	// Global cooldown check
	//	if (!_statsGeneral.CheckCooldown())
	//	{
	//		// Range Check
	//		if (Vector3.Distance(thisTransform.position, targetinfo.point) <= abilityRController.GetRange())
	//		{
	//			targetPoint = targetinfo.point;

	//			StatsGeneral tempstats = abilityRPrefab.GetComponent<StatsGeneral>();

	//			// Ability Cooldown Check and start
	//			if (!tempstats.CheckCooldown())
	//			{
	//				StopAllCoroutines();
	//				StartCoroutine(CastAbilityRCoroutine());
	//			}
	//		}
	//	}
	//}

	//public override IEnumerator CastAbilityRCoroutine()
	//{
	//	// Rotate
	//	Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
	//	targetRotation.x = 0;
	//	targetRotation.z = 0;
	//	thisTransform.rotation = targetRotation;

	//	// Cast ability
	//	GameObject clone = (GameObject)Instantiate(abilityRPrefab, targetPoint, thisTransform.rotation);

	//	cachedAbilityRController = clone.GetComponent<AbilityController>();

	//	// Set target and stats
	//	cachedAbilityRController.GenerateCombatStatistics(_statsGeneral, _statsOffense, _statsDefense);

	//	// Start global cooldown
	//	_statsGeneral.StartCooldown();

	//	yield return null;
	//}
}
#endregion
