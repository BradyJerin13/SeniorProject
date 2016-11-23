using UnityEngine;
using System.Collections;

public class MobController : MonoBehaviour 
{
	public GameObject shotSpawn;

	public GameObject basicAttack;
	public float basicAttackCoolDown;
	private float _basicAttackCoolDownTimer;

	public GameObject spell;
	public float spellCoolDown;
	private float _spellCoolDownTimer;



	public int stateMachine; 

	public float movementSpeed;
	public float wanderSpeed;
	public float directionChangeInterval = 1;
	public float movementSpeedDebuffDuration;

	public float minFollowDistance;
	public float maxAggroDistance;

	private float _movementSpeed;
	private float _movementSpeedDebuffDurationTimer = 0.0f;

	private bool debuff;
	private GameObject _targetAttack;
	private Vector3 _targetPosition;
	private Vector3 _originalPosition;

	// Use this for initialization
	void Start () 
	{
		_movementSpeed = movementSpeed;
		stateMachine = 0;

		_originalPosition = transform.position;

		// Set random initial rotation
		StartCoroutine(GenerateWanderPoints());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time > _movementSpeedDebuffDurationTimer && debuff == true)
		{
			_movementSpeed = movementSpeed;
			debuff = false;
		}

		_targetAttack = GameObject.FindGameObjectWithTag("Hero");

		// If there are targets to attack, Charge the unit
		if (_targetAttack != null)
		{
			float distanceFromTarget;
			distanceFromTarget = Vector3.Distance(transform.position, _targetAttack.transform.position);

			if (distanceFromTarget > maxAggroDistance)
			{
				stateMachine = 0;
				Move();
			}
			else if (distanceFromTarget > minFollowDistance)
			{
				stateMachine = 1;
				Move();
			}
			else
			{
				stateMachine = 2;
				Attack();
			}
		}
		else 
		{
			stateMachine = 0;
			Move();
		}
	}

	public void Move()
	{
		if (stateMachine == 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_targetPosition - transform.position), directionChangeInterval * Time.deltaTime);
			transform.position += transform.forward * wanderSpeed * Time.deltaTime;
		}
		else if (stateMachine == 1)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_targetAttack.transform.position - transform.position), 10 * Time.deltaTime);
			transform.position += transform.forward * _movementSpeed * Time.deltaTime;
		}
	}

	public void Attack()
	{
		if (Time.time > _basicAttackCoolDownTimer)
		{
			_basicAttackCoolDownTimer = Time.time + basicAttackCoolDown;

			TurnToAttack();
			Instantiate(basicAttack, shotSpawn.transform.position, shotSpawn.transform.rotation);
		}
		
		if (Time.time > _spellCoolDownTimer)
		{
			_spellCoolDownTimer = Time.time + spellCoolDown;

			TurnToAttack();
			Instantiate(spell, shotSpawn.transform.position, shotSpawn.transform.rotation);
		}
	}
	void TurnToAttack()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_targetAttack.transform.position - transform.position), 10 * Time.deltaTime);
	}

	IEnumerator GenerateWanderPoints()
	{
		while (true)
		{
			_targetPosition = _originalPosition + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Spell2"))
		{
			_movementSpeedDebuffDurationTimer = Time.time + movementSpeedDebuffDuration;
			_movementSpeed = 0.0f;
			debuff = true;
		}
	}
}
