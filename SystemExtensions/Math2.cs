using System;

namespace SystemExtensions
{
    /// <summary>
    /// More math methods.
    /// </summary>
    public static class Math2
    {
        public static int MinMax(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(value, max));
        }
    }
}