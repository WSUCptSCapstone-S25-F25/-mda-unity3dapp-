using System.Collections.Generic;
using UnityEngine;

namespace WhiskerComponents
{
    public class WhiskerPool : MonoBehaviour
    {
        public static WhiskerPool Instance { get; private set; }

        [Header("Pool Settings")]
        public GameObject whiskerPrefab;
        public int numSimulations;
        public int whiskersPerSimulation;

        private Whisker[] pool;
        private int poolSize;
        private int nextAvailableIndex = 0;
        private Queue<int> availableIndices = new Queue<int>();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public void InitializePool(int simCount, int whiskersPerSim)
        {
            numSimulations = simCount;
            whiskersPerSimulation = whiskersPerSim;
            poolSize = numSimulations * whiskersPerSimulation;

            pool = new Whisker[poolSize];
            availableIndices.Clear();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(whiskerPrefab, this.transform);
                obj.SetActive(false);
                Whisker whisker = obj.GetComponent<Whisker>();
                whisker.Index = i;
                pool[i] = whisker;
                availableIndices.Enqueue(i);
            }
        }

        // Get whiskers for a simulation
        public List<Whisker> AcquireWhiskers()
        {
            List<Whisker> whiskers = new List<Whisker>(whiskersPerSimulation);
            for (int i = 0; i < whiskersPerSimulation; i++)
            {
                if (availableIndices.Count == 0)
                {
                    Debug.LogError("No more whiskers available in pool!");
                    break;
                }
                int idx = availableIndices.Dequeue();
                Whisker whisker = pool[idx];
                whisker.gameObject.SetActive(true);
                whisker.Acquire();
                whiskers.Add(whisker);
            }
            return whiskers;
        }

        // Return whiskers to the pool after a simulation
        public void ReleaseWhiskers(List<Whisker> whiskers)
        {
            foreach (var whisker in whiskers)
            {
                whisker.Release();
                whisker.gameObject.SetActive(false);
                availableIndices.Enqueue(whisker.Index);
            }
        }
    }
}
