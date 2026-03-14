using Godot;
using System;

public partial class Hud : CanvasLayer
{
	

	[Signal]
	public delegate void EndTourEventHandler();

	[Signal]
	public delegate void TempEndTourEventHandler();

	[Signal]
	public delegate void UseSkillEventHandler(Attack skill);

	private VBoxContainer _skillContainer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_skillContainer = GetNode<VBoxContainer>("SkillContainer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void OnEndTourButtonPressed()
	{
		GetNode<Button>("EndTourButton").Hide();
		_skillContainer.Hide();
		EmitSignal(SignalName.EndTour);
	}

	public void ShowPlayerUI()
	{
		GetNode<Button>("EndTourButton").Show();
		FetchPlayerSkill();
				_skillContainer.Show();

		//Print other UI Stuff like HP, ATK,...
	}

	private void FetchPlayerSkill()
	{
				// Clear previous skill buttons
		foreach (Node child in _skillContainer.GetChildren())
		{
			child.QueueFree();
		}
		// Get player
		var players = GetTree().GetNodesInGroup("player");//Replace by a foreach
		if (players.Count > 0)
		{
			var player = players[0] as PlayerG;
			foreach (var playableUnit in players)
			{
				if (playableUnit is PlayerG playerUnit && playerUnit.PlayerSkill != null)
				{
					if (playerUnit.PlayerSkill.skill1 != null)
					{
						CreateSkillButton(playerUnit.PlayerSkill.skill1);
					}
					if (playerUnit.PlayerSkill.skill2 != null)
					{
						CreateSkillButton(playerUnit.PlayerSkill.skill2);
					}
				}else throw new Exception("Player unit not found or has no skills");

			}
		}
	}

	private void CreateSkillButton(Attack skill)
	{
		var button = new Button();
		button.Text = $"{skill.Name}\nMultiplier: {skill.MultiplierRaw}\nHits: {skill.NbHit}";
		button.Connect(Button.SignalName.Pressed, Callable.From(() => OnSkillButtonPressed(skill)));
		_skillContainer.AddChild(button);
	}

	private void OnSkillButtonPressed(Attack skill)
	{
		EmitSignal(SignalName.UseSkill, skill);
	}

	//Simule l'action d'un tour d'un méchant =(
	public void OnTempEndTourButtonPressed()
	{
		EmitSignal(SignalName.TempEndTour);
	}

}
