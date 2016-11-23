using UnityEngine;
using System.Collections;

public class FreezeQAbilityController : SkillShotController {

	public override void Hit()
	{
		// Receive ability
		_statsGeneralTarget.ReceiveHit(_statsGeneral);
		_statsOffenseTarget.ReceiveHit(_statsOffense);
		_statsDefenseTarget.ReceiveHit(_statsOffense, _statsDefense);

		Destroy(gameObject);
	}

	public override void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			Debug.Log("SKILL SHOT TRIGGER");
			SetTargetObject(other.gameObject);
			Hit();
		}
	}
}
