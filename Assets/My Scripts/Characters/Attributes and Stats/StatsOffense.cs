using UnityEngine;
using System.Collections;

public class StatsOffense : MonoBehaviour
{
	public Attributes attributes;

	// Base stats for the character
	public float _basePhysicalDamage = 1;
	public float _baseMagicDamage = 1;
	public float _baseCriticalDamage = 1;
	public float _baseCriticalChance = 1;
	public float _baseAttackSpeed = 1;
	public float _baseRange = 1;

	// Affected by Attributes and items
	public float _augmentedPhysicalDamage = 0;
	public float _augmentedMagicDamage = 0;
	public float _augmentedCriticalDamage = 0;
	public float _augmentedCriticalChance = 0;
	public float _augmentedAttackSpeed = 0;
	public float _augmentedRange = 0;

	// Affected by combat
	public float _combatPhysicalDamage = 0;
	public float _combatMagicDamage = 0;
	public float _combatCriticalDamage = 0;
	public float _combatCriticalChance = 0;
	public float _combatAttackSpeed = 0;
	public float _combatRange = 0;

	void Awake()
	{
		attributes = GetComponent<Attributes>();
		InitializeStats();
	}

	public void InitializeStats()
	{
		// Initialize augmented stats
		_augmentedPhysicalDamage = _basePhysicalDamage;
		_augmentedMagicDamage = _baseMagicDamage;
		_augmentedCriticalDamage = _baseCriticalDamage;
		_augmentedCriticalChance = _baseCriticalChance;
		_augmentedAttackSpeed = _baseAttackSpeed;
		_augmentedRange = _baseRange;

		// Initialize combat stats
		_combatPhysicalDamage = _augmentedPhysicalDamage;
		_combatMagicDamage = _augmentedMagicDamage;
		_combatCriticalDamage = _augmentedCriticalDamage;
		_combatCriticalChance = _augmentedCriticalChance;
		_combatAttackSpeed = _augmentedAttackSpeed;
		_combatRange = _augmentedRange;
	}

	/// <summary>
	/// Generates a Character's current statistics based on the attributes and items
	/// </summary>
	/// <param name="attributes">Character's attributes after items have been factored in</param>
	/// <param name="items">Character's items</param>
	public void GenerateAugmentedCharacterStatistics()
	{
		// Setting the baseline of the stats
		_augmentedPhysicalDamage = _basePhysicalDamage;
		_augmentedMagicDamage = _baseMagicDamage;
		_augmentedCriticalDamage = _baseCriticalDamage;
		_augmentedCriticalChance = _baseCriticalChance;
		_augmentedAttackSpeed = _baseAttackSpeed;
		_augmentedRange = _baseRange;

		// Factoring Attributes
		//_currentPhysicalDamage += _basePhysicalDamage;
		//_currentMagicDamage += _baseMagicDamage;
		
		//_currentRange += _baseRange;

		if (attributes != null)
		{
			_augmentedCriticalDamage += attributes._augmentedStrength * .05f + attributes._augmentedIntellect * .05f;
			_augmentedCriticalChance += attributes._augmentedDexterity * .05f + attributes._augmentedIntellect * .05f;
			_augmentedAttackSpeed += attributes._augmentedStrength * .05f + attributes._augmentedDexterity * .05f;
		}

		// Set the combat stats to the base combat stats
		_combatPhysicalDamage = _augmentedPhysicalDamage;
		_combatMagicDamage = _augmentedMagicDamage;
		_combatCriticalDamage = _augmentedCriticalDamage;
		_combatCriticalChance = _augmentedCriticalChance;
		_combatAttackSpeed = _augmentedAttackSpeed;
		_combatRange = _augmentedRange;
	}

	/// <summary>
	/// Generates an Ability's current statistics based on the stats
	/// </summary>
	/// <param name="statsOffense">Ability caster's offensive statistics</param>
	//public void GenerateCurrentAbilityStatistics(StatsOffense statsOffense)
	//{
	//	// Factor the caster's statistics into the ability's statistics
	//	_currentPhysicalDamage += statsOffense._currentPhysicalDamage;
	//	_currentMagicDamage += statsOffense._currentMagicDamage;
	//	_currentCriticalDamage += statsOffense._currentCriticalDamage;
	//	_currentCriticalChance += statsOffense._currentCriticalChance;
	//	_currentAttackSpeed += statsOffense._currentAttackSpeed;
	//	_currentCoolDownReduction += statsOffense._currentCoolDownReduction;
	//	_currentRange += statsOffense._currentRange;

	//	// Set the combat stats to the base combat stats
	//	_tempPhysicalDamage = _currentPhysicalDamage;
	//	_tempMagicDamage = _currentMagicDamage;
	//	_tempCriticalDamage = _currentCriticalDamage;
	//	_tempCriticalChance = _currentCriticalChance;
	//	_tempAttackSpeed = _currentAttackSpeed;
	//	_tempCoolDownReduction = _currentCoolDownReduction;
	//	_tempRange = _currentRange;
	//}


	/// <summary>
	/// Generates an Ability's temporary statistics
	/// </summary>
	/// <param name="statsOffense"></param>
	public void GenerateCombatAbilityStatistics(StatsOffense statsOffense)
	{
		// Set the combat stats to the base combat stats of the ability
		_combatPhysicalDamage = _augmentedPhysicalDamage;
		_combatMagicDamage = _augmentedMagicDamage;
		_combatCriticalDamage = _augmentedCriticalDamage;
		_combatCriticalChance = _augmentedCriticalChance;
		_combatAttackSpeed = _augmentedAttackSpeed;
		_combatRange = _augmentedRange;

		// Factor in the temporary statistics of the caster
		_combatPhysicalDamage += statsOffense._combatPhysicalDamage;
		_combatMagicDamage += statsOffense._combatMagicDamage;
		_combatCriticalDamage += statsOffense._combatCriticalDamage;
		_combatCriticalChance += statsOffense._combatCriticalChance;
		_combatAttackSpeed += statsOffense._combatAttackSpeed;
		_combatRange += statsOffense._combatRange;
	}

	/// <summary>
	/// Receives an ability hit, affecting statistics
	/// </summary>
	/// <param name="statsGeneral"></param>
	public void ReceiveHit(StatsOffense statsOffense)
	{
		// Debuffing
		//_tempPhysicalDamage -= statsOffense._tempPhysicalDamage;
		//_tempMagicDamage -= statsOffense._tempMagicDamage;
		//_tempCriticalDamage -= statsOffense._tempCriticalDamage;
		//_tempCriticalChance -= statsOffense._tempCriticalChance;
		//_tempAttackSpeed -= statsOffense._tempAttackSpeed;
		//_tempCoolDownReduction -= statsOffense._tempCoolDownReduction;
		//_tempRange -= statsOffense._tempRange;
	}

	public float GeneratePhysicalDamage()
	{
		float damage = _combatPhysicalDamage;

		// Roll for crit
		float random = Random.RandomRange(0.0f, 1.0f);

		// apply crit damage
		if (random <= _combatCriticalChance)
		{
			damage *= _combatCriticalDamage;
		}

		return damage;
	}

	public float GenerateMagicalDamage()
	{
		// Roll for crit
		// apply crit damage
		return _combatMagicDamage;
	}
}