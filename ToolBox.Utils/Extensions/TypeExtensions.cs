using System;

namespace ToolBox.Utils.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNumericType(this Type t)
        {
            return t == typeof(byte)
                || t == typeof(sbyte)
                || t == typeof(ushort)
                || t == typeof(uint)
                || t == typeof(uint)
                || t == typeof(short)
                || t == typeof(int)
                || t == typeof(long)
                || t == typeof(decimal)
                || t == typeof(float)
                || t == typeof(double);
        }
    }
}
