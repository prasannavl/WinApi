using System;

namespace Sample.DirectX
{
    public static class RandomExtensions
    {
        public static float NextFloat(this Random random)
        {
            return (float) random.NextDouble();
        }

        public static float NextFloat(this Random random, int min, int max)
        {
            return (float) random.NextDouble()*(max - min) + min;
        }
    }
}