using UnityEngine;
using System.Collections;

public class FreezeWAbilityController : AbilityController
{
	public float lifeTime;
	public float duration;

	public override void Start()
	{
		Debug.Log("Ability Controller start");
		lifeTime = Time.time + duration;
	}

	public override void Update()
	{
		if ( Time.time >= lifeTime)
		{
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		Collider[] others = Physics.OverlapSphere(transform.position, 2.0f);

		//if (_statsGeneral.CheckCooldown())
		//{
			foreach (Collider i in others)
			{
				if (i.gameObject.tag == "Enemy")
				{
					SetTargetObject(i.gameObject);
					Hit();
				}
			}
			//_statsGeneral.StartCooldown();
		//}

	}

	public override void Hit()
	{
		// Receive ability
		_statsGeneralTarget.ReceiveHit(_statsGeneral);
		_statsOffenseTarget.ReceiveHit(_statsOffense);
		_statsDefenseTarget.ReceiveHit(_statsOffense, _statsDefense);
	}
}

