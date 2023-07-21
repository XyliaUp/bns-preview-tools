using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

namespace FModel;
public static class Helper
{
    [StructLayout(LayoutKind.Explicit)]
    private struct NanUnion
    {
        [FieldOffset(0)]
        internal double DoubleValue;
        [FieldOffset(0)]
        internal readonly ulong UlongValue;
    }

    public static string FixKey(string key)
    {
        if (string.IsNullOrEmpty(key))
            return string.Empty;

        if (key.StartsWith("0x"))
            key = key[2..];

        return "0x" + key.ToUpper().Trim();
    }



    public static bool IsNaN(double value)
    {
        var t = new NanUnion { DoubleValue = value };
        var exp = t.UlongValue & 0xfff0000000000000;
        var man = t.UlongValue & 0x000fffffffffffff;
        return exp is 0x7ff0000000000000 or 0xfff0000000000000 && man != 0;
    }

    public static bool AreVirtuallyEqual(double d1, double d2)
    {
        if (double.IsPositiveInfinity(d1))
            return double.IsPositiveInfinity(d2);

        if (double.IsNegativeInfinity(d1))
            return double.IsNegativeInfinity(d2);

        if (IsNaN(d1))
            return IsNaN(d2);

        var n = d1 - d2;
        var d = (Math.Abs(d1) + Math.Abs(d2) + 10) * 1.0e-15;
        return -d < n && d > n;
    }

    public static float DegreesToRadians(float degrees)
    {
        return MathF.PI / 180f * degrees;
    }

    public static float RadiansToDegrees(float radians)
    {
        return radians* 180f / MathF.PI;
    }
}