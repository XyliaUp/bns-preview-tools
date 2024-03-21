namespace Xylia.Preview.UI.Controls.Helpers;
internal static class BooleanBoxes
{
    public const bool TrueBox = true;
    public const bool FalseBox = false;

    public static object Box(bool value)
    {
        if (value) return TrueBox;
        else return FalseBox;
    }
}