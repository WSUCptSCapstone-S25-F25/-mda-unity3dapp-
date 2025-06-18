using System;
using UnityEngine;
using WhiskerComponents;

namespace Util
{
    public static class WhiskerTransformerUtil
    {
        public static Quaternion NextAngle(System.Random rng)
        {
            return Quaternion.Euler(
                rng.Next(0, 360),
                rng.Next(0, 360),
                rng.Next(0, 360)
            );
        }    

        public static double NextLognormal(double mu, double sigma, System.Random rng)
        {
            double u1 = rng.NextDouble();
            double u2 = rng.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return Math.Exp(mu + sigma * randStdNormal);
        }

        /*public static Vector3 NextSpawnPosition(WhiskerState state, System.Random rng)
        {
            return state.SpawnAreaCenter + new Vector3(
                ((float)rng.NextDouble() - 0.5f) * state.SpawnAreaSize.x,
                ((float)rng.NextDouble() - 0.5f) * state.SpawnAreaSize.y,
                ((float)rng.NextDouble() - 0.5f) * state.SpawnAreaSize.z
            );
        }*/
    }
}