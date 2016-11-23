using UnityEngine;
using System.Collections;

public class StatsDefense : MonoBehaviour
{
	public Attributes attributes;

	// Base stats for the character
	public float _baseLife;
	public float _baseArmor;
	public float _baseMagicResist;

	// Affected by Attributes and items
	public float _augmentedLife;
	public float _augmentedArmor;
	public float _augmentedMagicResist;

	// Affected by combat
	public float _combatLife;
	public float _combatArmor;
	public float _combatMagicResist;

	void Awake()
	{
		attributes = GetComponent<Attributes>();
		InitializeStats();
	}

	public void InitializeStats()
	{
		// Initialize augmented stats
		_augmentedLife = _baseLife;
		_augmentedArmor = _baseArmor;
		_augmentedMagicResist = _baseMagicResist;

		// Initialize combat stats
		_combatLife = _augmentedLife;
		_combatArmor = _augmentedArmor;
		_combatMagicResist = _augmentedMagicResist;
	}


	/// <summary>
	/// Generates a characters current statistics
	/// </summary>
	/// <param name="attributes"></param>
	/// <param name="items"></param>
	public void GenerateAugmentedCharacterStatistics()
	{
		_augmentedLife = _baseLife;
		_augmentedArmor = _baseArmor;
		_augmentedMagicResist = _baseMagicResist;

		if (attributes != null)
		{
			// Attributes
			_augmentedArmor += attributes._augmentedStrength * .05f;
			_augmentedMagicResist += attributes._augmentedIntellect * .05f;
			_augmentedLife += attributes._augmentedWill * .05f;
		}

		_combatLife = _augmentedLife;
		_combatArmor = _augmentedArmor;
		_combatMagicResist = _augmentedMagicResist;
	}

	/// <summary>
	/// Generates an Ability's temporary statistics
	/// </summary>
	/// <param name="statsOffense"></param>
	public void GenerateCombatAbilityStatistics(StatsDefense statsDefense)
	{
		// Set the combat stats to the base combat stats of the ability
		_combatLife = _augmentedLife;
		_combatArmor = _augmentedArmor;
		_combatMagicResist = _augmentedMagicResist;

		// Factor in the temporary statistics of the caster
		//_tempLife += statsDefense._tempLife;
		//_tempArmor += statsDefense._tempArmor;
		//_tempMagicResist += statsDefense._tempMagicResist;
	}

	/// <summary>
	/// Receives an ability hit, affecting statistics
	/// </summary>
	/// <param name="statsGeneral"></param>
	public void ReceiveHit(StatsOffense statsOffense, StatsDefense statsDefense)
	{
		// Debuffing
		//_tempLife -= statsDefense._tempLife;
		//_tempArmor -= statsDefense._tempArmor;
		//_tempMagicResist -= statsDefense._tempMagicResist;

		// Generate the Damage
		float physicalDamage = statsOffense.GeneratePhysicalDamage();
		float magicalDamage = statsOffense.GenerateMagicalDamage();

		// Factor defensive statistics
		physicalDamage = physicalDamage / _combatArmor;
		magicalDamage = magicalDamage / _combatMagicResist;

		MenuManager.menuManager.DamageText(physicalDamage + magicalDamage, name);

		// Receiving the damage
		_combatLife -= (physicalDamage + magicalDamage);
	}

	public void ReceiveHeal(StatsOffense statsOffense, StatsDefense statsDefense)
	{
		// Generate the heal
		float physicalHeal = statsOffense.GeneratePhysicalDamage();
		float magicalHeal = statsOffense.GenerateMagicalDamage();

		// Receiving the Heal
		_combatLife += (physicalHeal + magicalHeal);
	}

	
}