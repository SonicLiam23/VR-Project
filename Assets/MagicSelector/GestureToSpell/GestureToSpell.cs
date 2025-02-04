// Name of Spell_Gesture
// AS LEFT/RIGHT IS SWAPPED IN LEFT HAND
// ALL DIRECTIONS ARE RELATIVE TO THE RIGHT HAND
public enum Spells
{
    NONE, Shield
}

public class GestureToSpell
{
    public static Spells ConvertGesture(Direction dir)
    {
        switch (dir)
        {
            case Direction.RIGHT_DOWN:
                return Spells.Shield;

            default:
                return Spells.NONE;
        }
    }
}
