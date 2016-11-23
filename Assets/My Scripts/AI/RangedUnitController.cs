using UnityEngine;
using System.Collections;

public class RangedUnitController : MonoBehaviour 
{
	public Transform targetMove;
	public GameObject targetAttack;
	private Transform unitTransform;

	public float rotationSpeed;
	public float movementSpeed;
	public float minFollowDistance;

	public GameObject building;

	private int state;


	void Awake()
	{
		unitTransform = transform;
	}


	// Use this for initialization
	void Start () 
	{
		state = 0;

		if(gameObject.CompareTag("Player1Unit"))
		{
			building = GameObject.FindGameObjectWithTag("Player1Building");
		}
		else if(gameObject.CompareTag("Player2Unit"))
		{
			building = GameObject.FindGameObjectWithTag("Player2Building");
		}

		targetMove = building.transform.Find("RallyPoint"); 
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (state == 0)
		{
			Debug.DrawLine(unitTransform.transform.position, targetMove.transform.position, Color.red);

			unitTransform.rotation = Quaternion.Slerp(unitTransform.rotation, Quaternion.LookRotation(targetMove.transform.position - unitTransform.position), rotationSpeed * Time.deltaTime);

			float distanceFromTarget;
			distanceFromTarget = Vector3.Distance(unitTransform.position, targetMove.transform.position);

			if (distanceFromTarget > minFollowDistance)
			{
				unitTransform.position += unitTransform.forward * movementSpeed * Time.deltaTime;
			}
			else 
			{
				state = 1;
			}
		}
		else if (state == 1)
		{
			if (gameObject.CompareTag("Player1Unit"))
			{
				targetAttack = GameObject.FindGameObjectWithTag("Player2Unit");
			}
			else if (gameObject.CompareTag("Player2Unit"))
			{
				targetAttack = GameObject.FindGameObjectWithTag("Player1Unit");
			}

			unitTransform.rotation = Quaternion.Slerp(unitTransform.rotation, Quaternion.LookRotation(targetAttack.transform.position - unitTransform.position), rotationSpeed * Time.deltaTime);

			Debug.DrawLine(unitTransform.transform.position, targetAttack.transform.position, Color.red);

			float distanceFromTarget;
			distanceFromTarget = Vector3.Distance(unitTransform.position, targetAttack.transform.position);

			if (distanceFromTarget > minFollowDistance)
			{
				unitTransform.position += unitTransform.forward * movementSpeed * Time.deltaTime;
			}
			else
			{ 
				
			}

			//state = 2;
		}
		else if (state == 2)
		{
			//state = 1;
		}

	}
}
