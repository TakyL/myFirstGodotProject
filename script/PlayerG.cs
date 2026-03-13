using Godot;
using System;
using System.Collections.Generic;
using System.Xml;

public partial class PlayerG : BaseCharacter
{

	private readonly Dictionary<string, Vector2> _inputs = new()
	{
		{ "move_right", Vector2.Right },
		{ "move_left",  Vector2.Left  },
		{ "move_down",  Vector2.Down  },
		{ "move_up",    Vector2.Up    }
	};

	private int _gridSize = 16;
	[Export] private RayCast2D _rayCast2D;

	public override void _Ready()
	{
		base._Ready();
		MotionMode = MotionModeEnum.Floating;
		_rayCast2D = GetNode<RayCast2D>("RayCast2D");
		AddToGroup("player");
		BeginTour();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (actifTour)
		{
			foreach (var action in _inputs.Keys)
			{
				if (@event.IsActionPressed(action))
				{
					HandleMoving(action);
				}
			}
		}
		
	}

	private void HandleMoving(string action)
	{
			Vector2 direction = _inputs[action] * _gridSize;
			Vector2 newPosition = Position + direction;

			if (!IsPositionColliding(direction))
			{
				UpdatePosition(newPosition);
			}
	}

	public bool IsPositionColliding(Vector2 direction)
	{
		_rayCast2D.TargetPosition = direction;
		_rayCast2D.ForceRaycastUpdate();
		return _rayCast2D.IsColliding();
	}

	public void BeginTour()
	{
		currentMoving = 0;
		actifTour = true;
	}

	public void EndTour()
	{
		actifTour = false;
	}
}
