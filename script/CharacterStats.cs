// CharacterStats.cs
using Godot;

[GlobalClass]
public partial class CharacterStats : Resource
{
	//[Export] public Sprite2D sprite {get ; set;} 
	[Export] public int MaxHealth { get; set; } = 100;
	[Export] public int MovingCase { get; set; } = 5;
	[Export] public int AttackValue { get; set; } = 10;
	[Export] public int DefValue {get;set;} = 5;
}
