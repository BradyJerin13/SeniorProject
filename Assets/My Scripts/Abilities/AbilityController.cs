using UnityEngine;
using System.Collections;

public class AbilityController : MonoBehaviour
{
	public Transform thisTransform;

	public StatsOffense _statsOffense;
	public StatsDefense _statsDefense;
	public StatsGeneral _statsGeneral;

	public bool lerp;

	public Vector3 targetPoint;
	public GameObject targetObject;

	public float targetDistance;

	public StatsOffense _statsOffenseTarget;
	public StatsDefense _statsDefenseTarget;
	public StatsGeneral _statsGeneralTarget;

	public float commandStartTime;
	public Vector3 commandStartPoint;
	public float commandDistance;

	public virtual void Awake()
	{
		Debug.Log("Ability Controller awake");
		thisTransform = transform;

		_statsOffense = GetComponent<StatsOffense>();
		_statsDefense = GetComponent<StatsDefense>();
		_statsGeneral = GetComponent<StatsGeneral>();
	}

	public virtual void Start()
	{
		Debug.Log("Ability Controller start");
		commandStartTime = Time.time;
		commandStartPoint = thisTransform.position;
		commandDistance = Vector3.Distance(commandStartPoint, targetPoint);
	}

	public virtual void Update()
	{
		if (targetObject == null)
		{
			Destroy(gameObject);
		}
		else if(targetObject != null)
		{
			Move();

			if (thisTransform.position == targetObject.transform.position && targetObject != null)
			{
				Hit();
			}
		}
	}

	/// <summary>
	/// Defines ability movement
	/// </summary>
	public virtual void Move()
	{
		Debug.Log("Ability Controller MOVE");
		if(targetObject != null)
			thisTransform.position = Vector3.MoveTowards(thisTransform.position, targetObject.transform.position, _statsGeneral._combatSpeed * Time.deltaTime);
	}

	public virtual void SetTargetObject(GameObject targetobject)
	{
		targetObject = targetobject;

		_statsGeneralTarget = targetObject.GetComponent<StatsGeneral>();
		_statsOffenseTarget = targetObject.GetComponent<StatsOffense>();
		_statsDefenseTarget = targetObject.GetComponent<StatsDefense>();
	}

	public virtual void SetTargetPoint(Vector3 targetpoint)
	{
		targetPoint = targetpoint;
	}

	public virtual void Hit()
	{ 
		// Receive ability
		_statsGeneralTarget.ReceiveHit(_statsGeneral);
		_statsOffenseTarget.ReceiveHit(_statsOffense);
		_statsDefenseTarget.ReceiveHit(_statsOffense, _statsDefense);

		Destroy(gameObject);
	}

	/// <summary>
	/// Generates the combat statistics for the ability
	/// </summary>
	/// <param name="statsGeneral"></param>
	/// <param name="statsOffense"></param>
	/// <param name="statsDefense"></param>
	public virtual void GenerateCombatStatistics(StatsGeneral statsGeneral, StatsOffense statsOffense, StatsDefense statsDefense)
	{
		_statsGeneral.GenerateCombatAbilityStatistics(statsGeneral);
		_statsOffense.GenerateCombatAbilityStatistics(statsOffense);
		_statsDefense.GenerateCombatAbilityStatistics(statsDefense);
	}

	public virtual float GetRange()
	{
		return GetComponent<StatsOffense>()._baseRange;
	}
}
