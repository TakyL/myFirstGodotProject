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

    public void UpdatePosition(Vector2 newPosition)
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
    /**
    *Return the position to the case format (16*16)
    */
    public Vector2I getPositionFormatCase()
    {
        //FIXME See if Global Position is better
        return  (Vector2I) Position / 16;
    }


}