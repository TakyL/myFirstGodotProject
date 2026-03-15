using Godot;


public partial class HudAction
{
	private CombatLogic combatLogic ;

	public HudAction(PlayerG player, Ennemydefault ennemy)
	{
		combatLogic = new CombatLogic(player, ennemy);
	}

	/**
	Detect if the click on skill is on an ennemy

	*/
	private bool clickOnEnemyDetected(Attack attackSelected, Vector2 mousePos, int gridSize, Godot.Collections.Array<Node> enemies)
	{
			GD.Print("click detected");
			int gridX = Mathf.FloorToInt(mousePos.X / gridSize);
			int gridY = Mathf.FloorToInt(mousePos.Y / gridSize);
			Vector2 targetPos = new(gridX * gridSize, gridY * gridSize);

			
			if(enemies == null)
			{
				GD.Print("No enemies found in the scene.");
				return false;
			}

			foreach (var e in enemies)
			{
				if (e is Node2D enemy)
				{
					CollisionShape2D collisionShape = enemy.GetNode<CollisionShape2D>("CollisionShape2D");
					if (collisionShape != null && collisionShape.Shape is RectangleShape2D rectShape)
					{
						Vector2 extents = rectShape.Size / 2;
						Transform2D transform = collisionShape.GlobalTransform;
						Vector2 localPoint = transform.AffineInverse() * mousePos;
						if (Mathf.Abs(localPoint.X) <= extents.X && Mathf.Abs(localPoint.Y) <= extents.Y)
						{
							GD.Print("it is an ennemy congrats");

							if(combatLogic.checkIfActionPossible(attackSelected.Range))
							{
								combatLogic.ApplyDamage(attackSelected);
							};
							return true;
						}
					}
				}
			}
			return false;
	}

	public bool HandleClick(InputEvent @event, World world)
	{
		if (world.selectingTarget && @event is InputEventMouseButton mb && mb.ButtonIndex == MouseButton.Left && mb.Pressed)
		{
			GD.Print("Skill selected" + world.currentSkill.Name);
			Vector2 mousePos = world.GetGlobalMousePosition();
			var enemies = world.GetTree().GetNodesInGroup("enemy");
			if (clickOnEnemyDetected(world.currentSkill, mousePos, world.gridSize, enemies))
			{
				world.selectingTarget = false;
				return true;
			}
		}
		return false;
	}   
}
