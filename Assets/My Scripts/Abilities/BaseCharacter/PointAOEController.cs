using UnityEngine;
using System.Collections;

public class PointAOEController : AbilityController
{
	public override void Awake()
	{
		thisTransform = transform;

		_statsOffense = GetComponent<StatsOffense>();
		_statsDefense = GetComponent<StatsDefense>();
		_statsGeneral = GetComponent<StatsGeneral>();
	}

	public override void Start()
	{

	}

	public override void Update()
	{
		Move();
	}

	/// <summary>
	/// Defines ability movement
	/// </summary>
	public override void Move()
	{
	}

	public override void SetTargetObject(GameObject targetobject)
	{
		targetObject = targetobject;

		_statsGeneralTarget = targetObject.GetComponent<StatsGeneral>();
		_statsOffenseTarget = targetObject.GetComponent<StatsOffense>();
		_statsDefenseTarget = targetObject.GetComponent<StatsDefense>();
	}

	public override void SetTargetPoint(Vector3 targetpoint)
	{
		targetPoint = targetpoint;
	}

	public override void Hit()
	{
		// Receive ability
		_statsGeneralTarget.ReceiveHit(_statsGeneral);
		_statsOffenseTarget.ReceiveHit(_statsOffense);
		_statsDefenseTarget.ReceiveHit(_statsOffense, _statsDefense);
	}

	/// <summary>
	/// Generates the combat statistics for the ability
	/// </summary>
	/// <param name="statsGeneral"></param>
	/// <param name="statsOffense"></param>
	/// <param name="statsDefense"></param>
	public override void GenerateCombatStatistics(StatsGeneral statsGeneral, StatsOffense statsOffense, StatsDefense statsDefense)
	{
		_statsGeneral.GenerateCombatAbilityStatistics(statsGeneral);
		_statsOffense.GenerateCombatAbilityStatistics(statsOffense);
		_statsDefense.GenerateCombatAbilityStatistics(statsDefense);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			SetTargetObject(other.gameObject);
			Hit();
		}
	}
}