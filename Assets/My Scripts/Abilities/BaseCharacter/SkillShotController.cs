using UnityEngine;
using System.Collections;

public class SkillShotController : AbilityController
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
		base.Start();
		Debug.Log("targetPoint" + targetPoint.ToString());
	//	targetPoint = commandStartPoint + _statsOffense._combatRange;

		Vector3 direction = thisTransform.forward;
		Vector3 endPoint = thisTransform.position + (direction.normalized * _statsOffense._combatRange);
		targetPoint = endPoint;
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

		float percentComplete = ((Time.time - commandStartTime) * _statsGeneral._combatSpeed) / _statsOffense._combatRange;

		thisTransform.position = Vector3.Lerp(commandStartPoint, targetPoint, percentComplete);

		if (thisTransform.position == targetPoint)
		{
			Destroy(gameObject);
		}
	}

	public override void SetTargetObject(GameObject targetobject)
	{
		targetObject = targetobject;

		_statsGeneralTarget = targetObject.GetComponent<StatsGeneral>();
		_statsOffenseTarget = targetObject.GetComponent<StatsOffense>();
		_statsDefenseTarget = targetObject.GetComponent<StatsDefense>();
	}

	public override void Hit()
	{
		// Receive ability
		_statsGeneralTarget.ReceiveHit(_statsGeneral);
		_statsOffenseTarget.ReceiveHit(_statsOffense);
		_statsDefenseTarget.ReceiveHit(_statsOffense, _statsDefense);

		Destroy(gameObject);
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			Debug.Log("SKILL SHOT TRIGGER");
			SetTargetObject(other.gameObject);
			Hit();
		}
	}

	//void OnTrigger(Collider other)
	//{
	//	Debug.Log("SKILL SHOT TRIGGER");
	//	Debug.Log(other.gameObject.tag.ToString());


	//	if (other.gameObject.tag == "Enemy")
	//	{
	//		Debug.Log("SKILL SHOT TRIGGER");
	//		SetTargetObject(other.gameObject);
	//		Hit();
	//	}
	//}
}