using System;
using Godot;

public partial class CombatLogic
{
	private BaseCharacter unit;

	private BaseCharacter target;


	public CombatLogic(PlayerG unit, Ennemydefault target)
	{
		this.unit = unit;
		this.target = target;
	
	}
	/*
	public static void ApplyDamage(BaseCharacterReturner target, int damage)
	{
		target.getCurrentHp() -= damage;
		GD.Print($"{target.unit.Name} takes {damage} damage! Remaining HP: {target.getCurrentHp()}");
		if (target.getCurrentHp() <= 0)
		{
			GD.Print($"{target.Name} has been defeated!");
			// Handle character defeat (e.g., remove from scene, play animation, etc.)
		}
	}*/

	public bool checkIfActionPossible(int attackRange )
	{

		if(attackRange > unit.currentMoving + unit.GetMoveCasePoints())
		{
			GD.Print("Action not possible: target is out of range.");
			return false;
		}
		return true;
	} 

	public void ApplyDamage(Attack attackSelected)
	{
		int damage = calculateDamage(attackSelected);
		target.setCurrentHealth(target.GetCurrentHealth() - damage);
		GD.Print($"{target.Name} takes {damage} damage! Remaining HP: {target.GetCurrentHealth()}");
	}

	private int calculateDamage(Attack attackSelected)
	{
		if(attackSelected.Type == AttackType.Physical)
		{
			// Calculate physical damage based on unit's stats and attack's multiplier
			return unit.Stats.AttackValue * (int)attackSelected.Multiplier - target.Stats.DefValue;
		}
		else if(attackSelected.Type == AttackType.Magical)
		{
			// Calculate magical damage based on unit's stats and attack's multiplier   
			return unit.Stats.MagicValue * (int)attackSelected.Multiplier - target.Stats.ResValue;
		}
		else
		{
			throw new NotImplementedException("TODO finish TRUE method calcul function");
		}
	}    
}
