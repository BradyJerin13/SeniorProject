using UnityEngine;
using System.Collections;

public class MeleeUnitController : MonoBehaviour 
{
	public Transform targetMove;
	public GameObject targetAttack;
	private Transform unitTransform;

	public float rotationSpeed;
	public float movementSpeed;
	public float minFollowDistance;
	public float health;

	public GameObject building;

	private int state;
	private string tag;

	void Awake()
	{
		unitTransform = transform;
	}


	void Start () 
	{
		state = 0;

		// Both Player1 and Player2 use the MeleeUnitController
		if(gameObject.CompareTag("Player1Unit"))
		{
			building = GameObject.FindGameObjectWithTag("Player1Building");
		}
	}
	
	void Update ()
	{
		

		// RallyPoint State
		// Unit Moves to RallyPoint
		if (state == 0)
		{
			targetMove = building.transform.Find("RallyPoint"); 

			Debug.DrawLine(unitTransform.transform.position, targetMove.transform.position, Color.red);

			unitTransform.rotation = Quaternion.Slerp(unitTransform.rotation, Quaternion.LookRotation(targetMove.transform.position - unitTransform.position), rotationSpeed * Time.deltaTime);

			float distanceFromTarget;
			distanceFromTarget = Vector3.Distance(unitTransform.position, targetMove.transform.position);

			// Continue moving until unit reaches rally point
			if (distanceFromTarget > minFollowDistance)
			{
				unitTransform.position += unitTransform.forward * movementSpeed * Time.deltaTime;
			}
			else 
			{
				state = 1;
			}
		}
		// Charge an "Enemy" unit
		// This depends on what side the unit is on
		else if (state == 1)
		{
			if (gameObject.CompareTag("Player1Unit"))
			{
				//targetAttack = GameObject.FindGameObjectWithTag("Player2Unit");
			}

			// If there are targets to attack, Charge the unit
			if (targetAttack != null)
			{
				unitTransform.rotation = Quaternion.Slerp(unitTransform.rotation, Quaternion.LookRotation(targetAttack.transform.position - unitTransform.position), rotationSpeed * Time.deltaTime);

				Debug.DrawLine(unitTransform.transform.position, targetAttack.transform.position, Color.red);

				float distanceFromTarget;
				distanceFromTarget = Vector3.Distance(unitTransform.position, targetAttack.transform.position);

				if (distanceFromTarget > minFollowDistance)
				{
					unitTransform.position += unitTransform.forward * movementSpeed * Time.deltaTime;
				}
			}
			else // If there are no targets to attack, Move back to RallyPoint
			{
				state = 0;
			}
		}
		else if (state == 2) // If the unit collided with another unit, Back up a distance
		{
			if (gameObject.CompareTag("Player1Unit"))
			{
				//targetAttack = GameObject.FindGameObjectWithTag("Player2Unit");
			}


			// If there are targets to attack, Back away from the unit
			if (targetAttack != null)
			{
				float distanceFromTarget;
				float moveAwayDistance = 1;
				unitTransform.position -= unitTransform.forward * movementSpeed * Time.deltaTime;
				distanceFromTarget = Vector3.Distance(unitTransform.position, targetAttack.transform.position);

				if (distanceFromTarget < moveAwayDistance)
				{
					unitTransform.position -= unitTransform.forward * movementSpeed * Time.deltaTime;
				}
				else
				{
					state = 1;
				}
			}
			else // If there are no targets to attack, Move back to RallyPoint
			{
				state = 0;
			}
		}
	}

	// Unit Collided with another unit
	void OnTriggerEnter(Collider other)
	{
		if (!gameObject.CompareTag(other.tag))
		{
			health -= Random.Range(0,5);

			// Only change to back up state if the unit was in charge state
			if(state == 1)
				state = 2;
		}

		// If the unit is dead, destroy it
		if (health <= 0)
		{
			gameObject.tag = "Dead";
			Destroy(gameObject);
		}
	}

	// Unit left the arena border
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Border")
		{
			Destroy(gameObject);
		}
	}
}
