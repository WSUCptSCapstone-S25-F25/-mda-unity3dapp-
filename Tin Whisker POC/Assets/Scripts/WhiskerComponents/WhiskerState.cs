using UnityEngine;

namespace WhiskerComponents
{
    [System.Serializable]
    public class WhiskerState
    {
        public int Amount;
        // Lognormal Parameters
        public float LengthMu, LengthSigma, WidthMu, WidthSigma;
        // Spawn Box Parameters
        public Vector3 SpawnVolumeSize, SpawnOrigin;
    }
}