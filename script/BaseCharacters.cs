// BaseCharacter.cs
using Godot;

public partial class BaseCharacter : CharacterBody2D
{
	[Export] public CharacterStats Stats { get; set; }

	protected int currentHealth { get; set; }

	protected int moveCasePoints { get; set; }
    public bool actifTour { get; set; } = false;
    public int currentMoving { get; set; } = 0;



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

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMoveCasePoints()
    {
        return moveCasePoints;
    }

	public void setCurrentHealth(int health)
	{
		currentHealth = health;
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