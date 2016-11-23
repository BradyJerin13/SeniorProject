using UnityEngine;
using System.Collections;

public class AbilityManager : MonoBehaviour
{
	private StatsOffense _statsOffense;
	private StatsDefense _statsDefense;
	private StatsGeneral _statsGeneral;

	private Transform thisTransform;

	void Awake()
	{
		thisTransform = transform;

		_statsOffense = GetComponent<StatsOffense>();
		_statsDefense = GetComponent<StatsDefense>();
		_statsGeneral = GetComponent<StatsGeneral>();
	}

	void Start()
	{

	}

	void Update()
	{

	}

	///// <summary>
	///// Generates the base combat stats for the Ability
	///// </summary>
	///// <param name="statsGeneral"></param>
	///// <param name="statsOffense"></param>
	///// <param name="statsDefense"></param>
	//public void GenerateAbilityStats(StatsGeneral statsGeneral, StatsOffense statsOffense, StatsDefense statsDefense)
	//{

	//}
}
