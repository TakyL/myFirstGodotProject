using System;
using System.Collections.Generic;
using Godot;
using System.Linq;

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
	/**
	*Check if the ennemy is in range
	*/
	private  bool checkIfActionPossible(int attackRange, int distanceToTarget )
	{
		//GD.Print("Checking Distance/AttackRange: " + distanceToTarget + ", " + attackRange);

		if(distanceToTarget > attackRange)  //IF enemy out of range
		{
			GD.Print("Action not possible: target is out of range.");
			return false;
		}
		GD.Print("Action possible: target is within range.");
		return true;
	} 
	public void moveNearTargetAndAttack(Attack attackSelected)
	{
		List<Vector2> casePoints =  new Pathfinder().FindShortestPath(unit.getPositionFormatCase(),target.getPositionFormatCase());
		//GD.Print("Max distance unit "+unit.GetMoveCasePoints(),"Distance to target "+casePoints.Count);
		int distanceCase = casePoints.Count - 1;  // Actual distance to target (path includes current position)
		Vector2 reste = unit.Position % 1f;
		
		
		foreach (Vector2 step in casePoints.Skip(1).Take(unit.GetMoveCasePoints()))
		{			
			if(checkIfActionPossible(attackSelected.Range,distanceCase)) 
			{	
				for(int i=0; i < attackSelected.NbHit; i++)
				{
					ApplyDamage(attackSelected);
				}	
				break;
			}
			else
			{
				Vector2 calcVec = step + reste ;
				unit.UpdatePosition(calcVec);	
				distanceCase--;
			} 
		}
	
	}


	protected void ApplyDamage(Attack attackUsed)
	{
		int damage = calculateDamage(attackUsed);
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
