using UnityEngine;
using System.Collections;

public class TargetHealController : AbilityController
{
	public override void Awake()
	{
		base.Awake();
	}

	public override void Start()
	{
		base.Start();
	}


	public override void Update()
	{
		Hit();
		Move();
	}

	/// <summary>
	/// Defines ability movement
	/// </summary>
	public override void Move()
	{
		float percentComplete = ((Time.time - commandStartTime) * _statsGeneral._combatSpeed) / commandDistance;

		thisTransform.position = Vector3.Lerp(commandStartPoint, targetPoint, percentComplete);

		if (thisTransform.position == targetPoint)
		{
			Destroy(gameObject);
		}
	}

	public override void SetTargetObject(GameObject targetobject)
	{
		targetObject = targetobject;
		targetPoint = transform.position + Vector3.up * targetDistance;

		_statsGeneralTarget = targetObject.GetComponent<StatsGeneral>();
		_statsOffenseTarget = targetObject.GetComponent<StatsOffense>();
		_statsDefenseTarget = targetObject.GetComponent<StatsDefense>();
	}

	public override void Hit()
	{
		// Receive Heal
		_statsDefenseTarget.ReceiveHeal(_statsOffense, _statsDefense);
	}
}
