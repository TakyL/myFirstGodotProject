using Godot;
using System;
using System.Collections.Generic;

public partial class Ennemydefault : BaseCharacter
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		AddToGroup("enemy");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlayTour()
	{
		//Check all the characters on the closest.
		//Find 

		var pathfinder = new Pathfinder();
		Vector2 resizePosition = Position / 16;
		Vector2 targetPosition = fetchPlayerPosition() / 16;
		Vector2 reste = resizePosition % 1f;

		GD.Print(resizePosition, targetPosition, reste);

		List<Vector2I> path = pathfinder.FindShortestPath((Vector2I)resizePosition,(Vector2I) targetPosition);

		foreach (Vector2I step in path)
		{			
		//	if(currentMoving != base.currentMoving)
			{
				Vector2 calcVec = step + reste;
				UpdatePosition(calcVec);
				GD.Print(calcVec);
			}
		}

	}
	//TEMP another class later 
	private Vector2 fetchPlayerPosition()
	{
		Node2D player = GetTree().GetFirstNodeInGroup("player") as Node2D;

		if (player == null) throw new Exception("Player not found"); // safety check

		return player.GlobalPosition;

	}


}
