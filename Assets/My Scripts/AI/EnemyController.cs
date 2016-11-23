using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour
{
    public int rotationSpeed;

	private Transform thisTransform;

	private Attributes _attributes;
	private StatsOffense _statsOffense;
	private StatsDefense _statsDefense;
	private StatsGeneral _statsGeneral;

	private bool _inCombat;
	public float aggroRange;
	
	public GameObject[] targets;
	public GameObject target;
	public float distanceFromTarget;

	public GameObject abilityBasicControllerPrefab;
	public AbilityController cachedAbilityBasicController;


    void Awake()
    {
		thisTransform = transform;
		_attributes = GetComponent<Attributes>();
		_statsOffense = GetComponent<StatsOffense>();
		_statsDefense = GetComponent<StatsDefense>();
		_statsGeneral = GetComponent<StatsGeneral>();

		_inCombat = false;
    }


    void Start()
    {
		targets = GameObject.FindGameObjectsWithTag("Friendly");
    }

    void Update()
    {
		if (targets.Length == 0)
		{
			targets = GameObject.FindGameObjectsWithTag("Friendly");
		}
		else 
		{
			target = GetClosestTarget();
		}


		distanceFromTarget = Vector3.Distance(thisTransform.position, target.transform.position);
		if(distanceFromTarget < 10)
		{
			if (distanceFromTarget < _statsOffense._augmentedRange && _inCombat == false)
			{
				// attack target
				StopAllCoroutines();
				StartCoroutine(Attack());
			}
			else if (distanceFromTarget > _statsOffense._augmentedRange)
			{
				thisTransform.position = Vector3.MoveTowards(thisTransform.position, target.transform.position, _statsGeneral._combatSpeed * Time.deltaTime);
			}
		}
		else 
		{
			_inCombat = false;
		}

		if (_statsDefense._combatLife <= 0)
		{
			LevelManager.levelManager.EnemyKillCount++;
			MenuManager.menuManager.UpdateObjectives();
			Destroy(gameObject);
		}
		

		//if (Time.time > fireRate)
		//{
		//	fireRate = Time.time + fireRate;
		//	Instantiate(selectedWeapon, selectedWeaponSpawn.position, selectedWeaponSpawn.rotation);
		//	audio.Play();
		//}

		//enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, Quaternion.LookRotation(target.position - enemyTransform.position), rotationSpeed * Time.deltaTime);
    
		//distanceFromTarget = Vector3.Distance (transform.position, target.transform.position);

		//if (distanceFromTarget > minFollowDistance) 
		//{
		//	enemyTransform.position += enemyTransform.forward * moveSpeed * Time.deltaTime;
		//}
    }

	public virtual IEnumerator Move()
	{
		yield return null;
	}
	public virtual IEnumerator Attack()
	{
		while (target != null)
		{
			if (!_statsGeneral.CheckCooldown())
			{
				_inCombat = true;

				StatsGeneral tempstats = abilityBasicControllerPrefab.GetComponent<StatsGeneral>();

				//Rotate to target
				Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
				targetRotation.x = 0;
				targetRotation.z = 0;
				thisTransform.rotation = targetRotation;

				// Cast ability
				GameObject clone = (GameObject)Instantiate(abilityBasicControllerPrefab, thisTransform.position, thisTransform.rotation);

				cachedAbilityBasicController = clone.GetComponent<AbilityController>();

				// Set target and stats
				cachedAbilityBasicController.SetTargetObject(target);
				cachedAbilityBasicController.GenerateCombatStatistics(_statsGeneral, _statsOffense, _statsDefense);

				// Start global cooldown
				_statsGeneral.StartGlobalCooldown(_statsOffense._combatAttackSpeed);
			}
			yield return null;
		}

		_inCombat = false;
	}

	public GameObject GetClosestTarget()
	{
		GameObject closestObject = null;

		foreach (GameObject t in targets)
		{
			if (closestObject == null)
			{
				closestObject = t;
			}
			else if (closestObject != null)
			{
				// If the new distance is less than its current target
				if (Vector3.Distance(thisTransform.position, t.transform.position) <= Vector3.Distance(thisTransform.position, closestObject.transform.position))
				{
					closestObject = t;
				}
			}
		}

		return closestObject;
	}



}
















