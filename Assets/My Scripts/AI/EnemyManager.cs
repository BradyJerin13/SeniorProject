using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	private Transform thisTransform;

	private Attributes _attributes;
	private StatsOffense _statsOffense;
	private StatsDefense _statsDefense;
	private StatsGeneral _statsGeneral;

	public int level;

	void Awake()
	{
		thisTransform = transform;
		level = 1;
		_attributes = GetComponent<Attributes>();
		_statsOffense = GetComponent<StatsOffense>();
		_statsDefense = GetComponent<StatsDefense>();
		_statsGeneral = GetComponent<StatsGeneral>();

		_attributes.InitializeEnemyAttributes(1);
		_statsOffense.GenerateAugmentedCharacterStatistics();
		_statsDefense.GenerateAugmentedCharacterStatistics();
		_statsGeneral.GenerateAugmentedCharacterStatistics();
	}

	public void UpdateEnemy(int enemylevel)
	{
		level = enemylevel;

		_attributes.InitializeEnemyAttributes(level);
		_statsOffense.GenerateAugmentedCharacterStatistics();
		_statsDefense.GenerateAugmentedCharacterStatistics();
		_statsGeneral.GenerateAugmentedCharacterStatistics();
	}
}
