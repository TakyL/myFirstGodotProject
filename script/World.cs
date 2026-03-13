using Godot;
using System;

public partial class World : Node2D
{
	private bool EnemyTour = false;
	private bool PlayerTour = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerTour = true;
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
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
