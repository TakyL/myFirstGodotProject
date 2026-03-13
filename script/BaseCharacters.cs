// BaseCharacter.cs
using Godot;

public partial class BaseCharacter : CharacterBody2D
{
	[Export] public CharacterStats Stats { get; set; }

	protected int currentHealth;

	protected int moveCasePoints;
    public bool actifTour = false;
    public int currentMoving = 0;



    public override void _Ready()
	{
		if (Stats == null)
		{
			GD.PrintErr("Forgot to assign the tres value in the inspector");
			return;
		}
		currentHealth = Stats.MaxHealth;
		moveCasePoints = Stats.MovingCase;		
	}

    protected void UpdatePosition(Vector2 newPosition)
    {
		if(currentMoving !=  moveCasePoints)
		{
            Position = newPosition;
            currentMoving++;
        }
        else
        {
            GD.Print("Max distance reached");
        }

    }

}