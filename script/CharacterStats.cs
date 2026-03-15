// CharacterStats.cs
using Godot;

[GlobalClass]
public partial class CharacterStats : Resource
{
	//[Export] public Sprite2D sprite {get ; set;} 
	[Export] public int MaxHealth;
	[Export] public int MovingCase;
	[Export] public int AttackValue { get; set; } = 10;
	[Export] public int DefValue {get;set;} = 5;
	[Export] public int MagicValue { get; set; } 
	[Export] public int ResValue { get; set; }
	[Export] public int SpeedValue { get; set; }
}
