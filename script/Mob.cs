using Godot;
using System;

public partial class Mob : RigidBody2D
{

	public AnimatedSprite2D getAnimatedSprite()
	{
		return GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string[] mobTypes = getAnimatedSprite().SpriteFrames.GetAnimationNames();
		getAnimatedSprite().Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//Free memory space cuz it's deleting the nodes when end frame
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}
