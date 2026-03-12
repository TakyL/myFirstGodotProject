// BaseCharacter.cs
using Godot;

public partial class BaseCharacter : CharacterBody2D
{
    [Export] public CharacterStats Stats { get; set; }

    protected int currentHealth;
    

    public override void _Ready()
    {
        currentHealth = Stats.MaxHealth;
    }
}