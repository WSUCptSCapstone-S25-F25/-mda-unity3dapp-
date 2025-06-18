using System.Collections.Generic;
using UnityEngine;
using Util;
using WhiskerComponents;

namespace TransformerComponents
{
    public class WhiskerTransformer : MonoBehaviour
    {
        public static WhiskerTransformer Instance { get; private set; }

        private System.Random rng;

        [Header("Random Seed (set for reproducibility, leave 0 for random)")]
        public int seed = 0;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (seed != 0)
                rng = new System.Random(seed);
            else
                rng = new System.Random();
        }

        public void Reseed(int newSeed)
        {
            seed = newSeed;
            rng = new System.Random(seed);
        }

        /*public void TransformWhiskers(List<Whisker> whiskers, WhiskerState state)
        {
            for (int i = 0; i < whiskers.Count; i++)
            {
                var whisker = whiskers[i];
                double length = WhiskerTransformerUtil.NextLognormal(state.LengthMu, state.LengthSigma, rng);
                double width  = WhiskerTransformerUtil.NextLognormal(state.WidthMu, state.WidthSigma, rng);
                whisker.transform.position = WhiskerTransformerUtil.GetRandomSpawnPosition(state, rng);
                whisker.transform.rotation = WhiskerTransformerUtil.GetRandomRotation(rng);
                whisker.transform.localScale = new Vector3(width, length, width);
            }
        }*/
    }
}
