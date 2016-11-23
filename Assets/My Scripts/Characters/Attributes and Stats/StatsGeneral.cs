using UnityEngine;
using System.Collections;

public class StatsGeneral : MonoBehaviour
{
	public Attributes attributes;

	// Base stats for the character
	public float _baseSpeed;
	public float _baseCoolDownDuration;
	public float _baseCoolDownReduction;

	// Affected by Attributes and items
	public float _augmentedSpeed;
	public float _augmentedCoolDownDuration;
	public float _augmentedCoolDownReduction;

	// Affected by combat
	public float _combatSpeed;
	public float _combatCooldownDuration;
	public float _combatCoolDownReduction;

	public float coolDownTime;

	void Awake()
	{
		attributes = GetComponent<Attributes>();
		InitializeStats();
	}

	public void InitializeStats()
	{
		// Initialize augmented stats
		_augmentedSpeed = _baseSpeed;
		_augmentedCoolDownDuration = _baseCoolDownDuration;
		_augmentedCoolDownReduction = _baseCoolDownReduction;

		// Initialize combat stats
		_combatSpeed = _augmentedSpeed;
		_combatCooldownDuration = _augmentedCoolDownDuration;
		_combatCoolDownReduction = _augmentedCoolDownReduction;
	}


	public void GenerateAugmentedCharacterStatistics()
	{
		_augmentedSpeed = _baseSpeed;
		_augmentedCoolDownDuration = _baseCoolDownDuration;
		_augmentedCoolDownReduction = _baseCoolDownReduction;

		// Attributes
		if (attributes != null)
		{
			_augmentedCoolDownReduction += attributes._augmentedDexterity * .05f + attributes._augmentedWill * .05f;
		}

		//_augmentedCoolDownDuration -= _augmentedCoolDownDuration * _augmentedCoolDownReduction;
		_combatCooldownDuration = _augmentedCoolDownDuration;
		_combatCoolDownReduction = _augmentedCoolDownReduction;
	}

	/// <summary>
	/// Generates an Ability's temporary statistics
	/// </summary>
	/// <param name="statsGeneral"></param>
	public void GenerateCombatAbilityStatistics(StatsGeneral statsGeneral)
	{
		// Set the combat stats to the base combat stats of the ability
		_combatSpeed = _augmentedSpeed;
		_combatCoolDownReduction = _augmentedCoolDownReduction;
		_combatCooldownDuration = _augmentedCoolDownDuration;

		// Factor in the temporary statistics of the caster
		//_tempSpeed += statsGeneral._tempSpeed;
		_combatCoolDownReduction += statsGeneral._combatCoolDownReduction;
		//_combatCooldownDuration = _combatCooldownDuration * _combatCoolDownReduction;
	}


	/// <summary>
	/// Receives an ability hit, affecting statistics
	/// </summary>
	/// <param name="statsGeneral"></param>
	public void ReceiveHit(StatsGeneral statsGeneral)
	{
		// Debuffing
		//_tempSpeed -= statsGeneral._tempSpeed;
	}

	public bool StartCooldown()
	{
		if (!CheckCooldown())
		{
			coolDownTime = Time.time + _combatCooldownDuration;
			return false; // There was no active cooldown
		}

		return true; // There was an active cooldown
	}

	public bool CheckCooldown()
	{
		if (Time.time > coolDownTime)
		{
			return false; // There was no active cooldown
		}

		return true; // There was an active cooldown
	}

	public bool StartGlobalCooldown(float attackspeed)
	{
		_combatCooldownDuration = _augmentedCoolDownDuration;

		if (!CheckCooldown())
		{
			_combatCooldownDuration = _combatCooldownDuration / attackspeed;
			coolDownTime = Time.time + _combatCooldownDuration;
			return false; // There was no active cooldown
		}

		return true; // There was an active cooldown
	}

	public float GetGlobalCooldown(float attackspeed)
	{ 
		_combatCooldownDuration = _augmentedCoolDownDuration;

		_combatCooldownDuration = _combatCooldownDuration / attackspeed;
		return _combatCooldownDuration;
	}
}