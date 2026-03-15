using Godot;

public partial class BaseCharacterReturner
{
    public BaseCharacter unit;

    public BaseCharacterReturner(BaseCharacter unit)
    {
        this.unit = unit;
    }

    public int getCurrentHp()
    {
        return unit.GetCurrentHealth();
    }

    public int getMaxHp()
    {
        return unit.Stats.MaxHealth;
    }

    public int getMaxMovePoints()
    {
        return unit.GetMoveCasePoints();
    }

    public int getCurrentMovePoints()
    {
        return unit.currentMoving;
    }


}