using UnityEngine;
using System.Collections;

/// <summary>
/// Character Controller Class defines what a character does.
/// </summary>
public class CharacterController : MonoBehaviour
{
	public Transform thisTransform;

	// Attributes and Statistics
	public Attributes _attributes;
	public StatsOffense _statsOffense;
	public StatsDefense _statsDefense;
	public StatsGeneral _statsGeneral;

	// Abilities
	public GameObject basicAbilityPrefab;
	public GameObject abilityQPrefab;
	public GameObject abilityWPrefab;
	public GameObject abilityEPrefab;
	public GameObject abilityRPrefab;

	// Cache last used ability controllers
	public AbilityController abilityBasicController;
	public AbilityController abilityQController;
	public AbilityController abilityWController;
	public AbilityController abilityEController;
	public AbilityController abilityRController;

	public AbilityController cachedAbilityBasicController;
	public AbilityController cachedAbilityQController;
	public AbilityController cachedAbilityWController;
	public AbilityController cachedAbilityEController;
	public AbilityController cachedAbilityRController;

	// Cache last used target object
	public GameObject targetObject;

	// Cache last used target point
	public Vector3 targetPoint;

	// Cache the distance from the target object or point
	public float targetDistance;


	public float commandStartTime;
	public Vector3 commandStartPoint;
	public float commandDistance;

	//private Vector3 placePosition;
	//private Quaternion placeRoatation;
	//private bool placeFlag;

	public bool moving;
	public bool attacking;
	public bool interacting;
	public bool idle;

	public bool human;
	public GameObject[] enemyTargets;
	public GameObject[] friendlyTargets;
	public float distanceFromTarget;
	public Vector3 formationPoint;

	public virtual void Awake()
	{
		thisTransform = transform;
		targetPoint = thisTransform.position;

		_attributes = GetComponent<Attributes>();
		_statsOffense = GetComponent<StatsOffense>();
		_statsDefense = GetComponent<StatsDefense>();
		_statsGeneral = GetComponent<StatsGeneral>();
	}

	public virtual void Start()
	{
		idle = true;
		enemyTargets = GameObject.FindGameObjectsWithTag("Enemy");
		friendlyTargets = GameObject.FindGameObjectsWithTag("Friendly");
	}

		
	public virtual void Update()
	{
		// keep track of the distance between this gameObject target point
		targetDistance = Vector3.Distance(thisTransform.position, targetPoint);

		if (human != true)
		{
			friendlyTargets = GameObject.FindGameObjectsWithTag("Friendly");
			enemyTargets = GameObject.FindGameObjectsWithTag("Enemy");

			if (enemyTargets.Length != 0)
			{
				targetObject = GetClosestTarget(enemyTargets);

				distanceFromTarget = Vector3.Distance(thisTransform.position, targetObject.transform.position);
				if (distanceFromTarget < 10)
				{
					attacking = true;
					//if (distanceFromTarget < _statsOffense._augmentedRange)
					//{
						// attack target
						CastBasicAbility(targetObject);
					//}
					//else if (distanceFromTarget > _statsOffense._augmentedRange)
					//{
					//	thisTransform.position = Vector3.MoveTowards(thisTransform.position, targetObject.transform.position, _statsGeneral._combatSpeed * Time.deltaTime);
					//}

				}
				else
				{
					StopAllCoroutines();
					idle = true;
					//targetObject = GetClosestTarget(friendlyTargets);
					foreach (GameObject friend in friendlyTargets)
					{
						if (friend != null)
						{
							if (friend.GetComponent<CharacterController>().human == true)
							{
								targetObject = friend;
							}
						}
						
					}

					thisTransform.position = Vector3.MoveTowards(thisTransform.position, targetObject.transform.position + formationPoint, _statsGeneral._combatSpeed * Time.deltaTime);
				}
			}
			else
			{
				StopAllCoroutines();
				idle = true;
				//targetObject = GetClosestTarget(friendlyTargets);
				foreach (GameObject friend in friendlyTargets)
				{
					if (friend.GetComponent<CharacterController>().human == true)
					{
						targetObject = friend;
					}
				}

				thisTransform.position = Vector3.MoveTowards(thisTransform.position, targetObject.transform.position + formationPoint, _statsGeneral._combatSpeed * Time.deltaTime);
			}
			
			

			
		}

		if (_statsDefense._combatLife <= 0)
		{
			LevelManager.levelManager.CharacterDeathCount++;
			MenuManager.menuManager.UpdateObjectives();
			Destroy(gameObject);
		}
		// Global Cooldown Check
		//if (!_statsGeneral.CheckCooldown())
		//{
		//	if (!idle)
		//	{

		//	}
		//}

		
	}


	public virtual void RightClick(RaycastHit targetInfo)
	{
		idle = false;

		if (targetInfo.collider.tag == "Enemy")
		{
			targetObject = targetInfo.collider.gameObject;


			Debug.Log("CASTING!");
			CastBasicAbility(targetObject);

			attacking = true;
			moving = false;
			interacting = false;
		}
		else if (targetInfo.collider.tag == "Interactable")
		{
			StopAllCoroutines();
			targetObject = targetInfo.collider.gameObject;
			Interact();

			interacting = true;
			attacking = false;
			moving = false;
		}
		else
		{
			targetPoint = targetInfo.point;

			targetPoint.y = 1;

			StopAllCoroutines();
			StartCoroutine(Move());

			moving = true;
			attacking = false;
			interacting = false;
		}
	}

	public virtual void Interact()
	{
		if (targetObject.tag == "Interactable")
		{
			InteractableController interactObject = targetObject.GetComponent<InteractableController>();
			interactObject.Interact(gameObject);

			//if (targetObject.name == "Interactable")
			//{
			//	Debug.Log("Interacting");
			//	InteractableController interactObject = targetObject.GetComponent<InteractableController>();
			//	interactObject.Interact(gameObject);
			//}
			//else if (targetObject.name == "Person")
			//{
			//	Debug.Log("Interacting");
			//	InteractableController interactObject = targetObject.GetComponent<InteractableController>();
			//	interactObject.Interact(gameObject);
			//}
			
		}
		idle = true;
	}

	public virtual IEnumerator Move()
	{
		commandStartTime = Time.time;
		commandStartPoint = thisTransform.position;
		commandDistance = Vector3.Distance(commandStartPoint, targetPoint);

		// Turn
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
		targetRotation.x = 0;
		targetRotation.z = 0;

		thisTransform.rotation = targetRotation;

		while (targetDistance >= 0.5f)
		{
			float percentComplete = ((Time.time - commandStartTime) * _statsGeneral._combatSpeed) / commandDistance;

			if (percentComplete != 0)
			{
				thisTransform.position = Vector3.Lerp(commandStartPoint, targetPoint, percentComplete);
			}

			yield return null;
		}


		idle = true;
		moving = false;
	}

	public virtual void CastBasicAbility(GameObject targetobject)
	{
		// Global cooldown check
		if (!_statsGeneral.CheckCooldown())
		{
			// Target Check
			if (targetobject.tag == "Enemy")
			{
				// Set target
				targetObject = targetobject;

				// Range Check
				if (Vector3.Distance(thisTransform.position, targetObject.transform.position) <= _statsOffense._combatRange)
				{
					StopAllCoroutines();
					StartCoroutine(CastAbilityBasicCoroutine());
				}
				else if (Vector3.Distance(thisTransform.position, targetObject.transform.position) > _statsOffense._combatRange)
				{
					thisTransform.position = Vector3.MoveTowards(thisTransform.position, targetObject.transform.position, _statsGeneral._combatSpeed * Time.deltaTime);
				}
			}
		}
	}
	public virtual IEnumerator CastAbilityBasicCoroutine()
	{
		while (targetObject != null)
		{
			if (!_statsGeneral.CheckCooldown())
			{
				StatsGeneral tempstats = basicAbilityPrefab.GetComponent<StatsGeneral>();

				//Rotate to target
				Quaternion targetRotation = Quaternion.LookRotation(targetObject.transform.position - transform.position);
				targetRotation.x = 0;
				targetRotation.z = 0;
				thisTransform.rotation = targetRotation;

				Debug.Log("PEW");
				
				// Cast ability
				GameObject clone = (GameObject)Instantiate(basicAbilityPrefab, thisTransform.position, thisTransform.rotation);

				cachedAbilityBasicController = clone.GetComponent<AbilityController>();

				// Set target and stats
				cachedAbilityBasicController.SetTargetObject(targetObject);
				cachedAbilityBasicController.GenerateCombatStatistics(_statsGeneral, _statsOffense, _statsDefense);

				// Start global cooldown
				_statsGeneral.StartGlobalCooldown(_statsOffense._combatAttackSpeed);
			}
			yield return null;
		}
	}


	public virtual void CastAbilityQ(RaycastHit targetinfo)
	{
		// Global cooldown check
		if (!_statsGeneral.CheckCooldown())
		{
			// Set target
			targetPoint = targetinfo.point;

			StatsGeneral tempstats = abilityQPrefab.GetComponent<StatsGeneral>();

			// Ability Cooldown Check and start
			if (!tempstats.CheckCooldown())
			{
				StopAllCoroutines();
				StartCoroutine(CastAbilityQCoroutine(targetinfo));
			}
		}
	}

	public virtual IEnumerator CastAbilityQCoroutine(RaycastHit targetinfo)
	{
		// Rotate
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
		targetRotation.x = 0;
		targetRotation.z = 0;
		thisTransform.rotation = targetRotation;

		// Cast ability
		GameObject clone = (GameObject)Instantiate(abilityQPrefab, thisTransform.position, thisTransform.rotation);

		cachedAbilityQController = clone.GetComponent<AbilityController>();

		// Set target and stats
		cachedAbilityQController.GenerateCombatStatistics(_statsGeneral, _statsOffense, _statsDefense);

		// Start global cooldown
		_statsGeneral.StartCooldown();
		
		yield return null;
	}


	/// <summary>
	/// Target Heal Ability
	/// 
	/// This is just a sample method for casting an ability
	/// </summary>
	/// <param name="targetinfo"></param>
	public virtual void CastAbilityW(RaycastHit targetinfo)
	{
		// Global cooldown check
		if (!_statsGeneral.CheckCooldown())
		{
			// Target check
			if (targetinfo.collider.gameObject.tag == "Friendly")
			{
				// Set target
				targetObject = targetinfo.collider.gameObject;

				// Range Check
				if (targetDistance <= abilityWController.GetRange())
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
		}
	}

	public virtual IEnumerator CastAbilityWCoroutine()
	{
		// Rotate
		Quaternion targetRotation = Quaternion.LookRotation(targetObject.transform.position - transform.position);
		targetRotation.x = 0;
		targetRotation.z = 0;
		thisTransform.rotation = targetRotation;

		// Cast ability
		GameObject clone = (GameObject)Instantiate(abilityWPrefab, targetObject.transform.position, thisTransform.rotation);

		cachedAbilityWController = clone.GetComponent<AbilityController>();

		// Set target and stats
		cachedAbilityWController.SetTargetObject(targetObject);
		cachedAbilityWController.GenerateCombatStatistics(_statsGeneral, _statsOffense, _statsDefense);

		// Start global cooldown
		_statsGeneral.StartCooldown();

		yield return null;
	}

	public virtual void CastAbilityE(RaycastHit targetinfo)
	{
		// Global cooldown check
		if (!_statsGeneral.CheckCooldown())
		{
			// Target check
			if (targetinfo.collider.gameObject.tag == "Enemy")
			{
				// Set target
				targetObject = targetinfo.collider.gameObject;
				//targetPoint = targetpoint;

				// Range Check

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

	public virtual IEnumerator CastAbilityECoroutine()
	{
		// Rotate
		Quaternion targetRotation = Quaternion.LookRotation(targetObject.transform.position - transform.position);
		targetRotation.x = 0;
		targetRotation.z = 0;
		thisTransform.rotation = targetRotation;

		// Cast ability
		GameObject clone = (GameObject)Instantiate(abilityEPrefab, thisTransform.position, thisTransform.rotation);

		cachedAbilityEController = clone.GetComponent<AbilityController>();

		// Set target and stats
		cachedAbilityEController.SetTargetObject(targetObject);
		cachedAbilityEController.GenerateCombatStatistics(_statsGeneral, _statsOffense, _statsDefense);

		// Start global cooldown
		_statsGeneral.StartCooldown();

		yield return null;
	}


	public virtual void CastAbilityR(RaycastHit targetinfo)
	{
		// Global cooldown check
		if (!_statsGeneral.CheckCooldown())
		{
			// Range Check
			if (Vector3.Distance(thisTransform.position, targetinfo.point) <= abilityRController.GetRange()) 
			{
				targetPoint = targetinfo.point;

				StatsGeneral tempstats = abilityRPrefab.GetComponent<StatsGeneral>();

				// Ability Cooldown Check and start
				if (!tempstats.CheckCooldown())
				{
					StopAllCoroutines();
					StartCoroutine(CastAbilityRCoroutine());
				}
			}
		}
	}

	public virtual IEnumerator CastAbilityRCoroutine()
	{
		// Rotate
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
		targetRotation.x = 0;
		targetRotation.z = 0;
		thisTransform.rotation = targetRotation;

		// Cast ability
		GameObject clone = (GameObject)Instantiate(abilityRPrefab, targetPoint, thisTransform.rotation);

		cachedAbilityRController = clone.GetComponent<AbilityController>();

		// Set target and stats
		cachedAbilityRController.GenerateCombatStatistics(_statsGeneral, _statsOffense, _statsDefense);

		// Start global cooldown
		_statsGeneral.StartCooldown();

		yield return null;
	}


	public GameObject GetClosestTarget(GameObject[] targets)
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


	public virtual void SetAbilities(GameObject basic, GameObject Q, GameObject W, GameObject E, GameObject R)
	{
		basicAbilityPrefab = basic;
		abilityQPrefab = Q;
		abilityWPrefab = W;
		abilityEPrefab = E;
		abilityRPrefab = R;

		abilityBasicController = basicAbilityPrefab.GetComponent<AbilityController>();
		abilityQController = abilityQPrefab.GetComponent<AbilityController>();
		abilityWController = abilityWPrefab.GetComponent<AbilityController>();
		abilityEController = abilityEPrefab.GetComponent<AbilityController>();
		abilityRController = abilityRPrefab.GetComponent<AbilityController>();
	}
}