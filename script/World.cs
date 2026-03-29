using Godot;
using System;

public partial class World : Node2D
{
	private bool EnemyTour = false;
	private bool PlayerTour = false;
	public bool selectingTarget = false;
	public Attack currentSkill;
	public int gridSize = 16;
	private GridOverlay _gridOverlay;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerTour = true;
		GetNode<Hud>("HUD").Connect("UseSkill", new Callable(this, "OnUseSkill"));
		StartPlayerTour();

		_gridOverlay = new GridOverlay();
		_gridOverlay.Name = "WorldGridOverlay";
		_gridOverlay.CellSize = gridSize;
		_gridOverlay.LineColor = new Color(1f, 1f, 1f, 0.35f);
		_gridOverlay.LineWidth = 1f;

		ConfigureWorldGridOverlay();

		AddChild(_gridOverlay);
		_gridOverlay.ZIndex = 100; // Draw on top of world tiles
		_gridOverlay.QueueRedraw();
	}

	private void ConfigureWorldGridOverlay()
	{
		var tileMap = GetNodeOrNull<TileMapLayer>("TileMapLayer");

		if (tileMap != null)
		{
			var usedRect = tileMap.GetUsedRect();
			_gridOverlay.CellSize = gridSize;
			_gridOverlay.Columns = Math.Max(1, usedRect.Size.X);
			_gridOverlay.Rows = Math.Max(1, usedRect.Size.Y);

			// The TileMap origin may not be at world origin; adjust overlay position accordingly.
			var mapTopLeft = tileMap.MapToLocal(new Vector2I(usedRect.Position.X, usedRect.Position.Y));
			_gridOverlay.Position = tileMap.Position + mapTopLeft;
		}
		else
		{
			// Fallback to 16x16 world grid at origin
			_gridOverlay.Columns = 16;
			_gridOverlay.Rows = 16;
			_gridOverlay.Position = Vector2.Zero;
		}

		_gridOverlay.QueueRedraw();
	} 


	public void StartPlayerTour()
	{
		GetNode<Hud>("HUD").ShowPlayerUI();
		GetNode<PlayerG>("Player").BeginTour();
		PlayerTour = true;

	}


	public void HandleSwitchTour()
	{
		GD.Print("DETECTE SWITCH TOUR");
		if (PlayerTour)
		{
			EnemyTour=true;
			PlayerTour=false;
			GetNode<PlayerG>("Player").EndTour();
			GD.Print("Le tour eest à l'ennemy");
			GetNode<Ennemydefault>("Enemy").PlayTour();

		}
		else if (EnemyTour) 
		{
			EnemyTour=false;
			PlayerTour=true;
			GD.Print("Le tour est au joueur");
			StartPlayerTour();
		}

	}

	public override void _UnhandledInput(InputEvent @event)
	{
		new HudAction(GetNode<PlayerG>("Player"), GetNode<Ennemydefault>("Enemy")).HandleClick(@event, this);
	}

	private void OnUseSkill(Attack skill)
	{
		selectingTarget = true;
		currentSkill = skill;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
