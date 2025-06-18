using System;
using System.Collections;
using LoggingComponents;
using Model;
using TMPro;
using UnityEngine;

namespace WhiskerSimComponents
{
    public class MonteCarloSim : MonoBehaviour
    {
        private const string channelName = "MonteCarlo";

        public int numSimulations = 2;
        public TextMeshProUGUI NumSimsProgressText;
        public bool IsSimulationEnded;

        private const int MAX_BATCH_SIZE = 10;

        public void RunMonteCarloSim(WhiskerSim whiskerSim, int simNumber, float duration)
        {
            DateTime simulationStart = DateTime.Now;
            LoggingManager.Log(channelName, $"[Simulation {simNumber}] Starting Monte Carlo with {numSimulations} simulations.\n");

            IsSimulationEnded = false;
            Time.timeScale = 10.0f;
            StartCoroutine(RunBatches(whiskerSim, simNumber, duration, simulationStart));
        }

        private IEnumerator RunBatches(WhiskerSim whiskerSim, int simNumber, float duration, DateTime simulationStart)
        {
            int totalSimulations = numSimulations;
            int batchStart = simNumber;
            int simsCompleted = 0;
            int currentBatch = 0;
            int totalBatches = totalSimulations / MAX_BATCH_SIZE;
            if (totalSimulations % MAX_BATCH_SIZE != 0) totalBatches++;

            NumSimsProgressText.text = $"{simsCompleted} of {numSimulations} Completed";

            while (batchStart < simNumber + totalSimulations)
            {
                currentBatch++;
                int batchEnd = Mathf.Min(batchStart + MAX_BATCH_SIZE, simNumber + totalSimulations);

                DateTime batchStartTime = DateTime.Now;
                yield return RunBatch(whiskerSim, batchStart, batchEnd, duration);
                DateTime batchEndTime = DateTime.Now;

                double batchDuration = (batchEndTime - batchStartTime).TotalSeconds;
                LoggingManager.Log(channelName, $"[Simulation {simNumber}] Completed batch {currentBatch}/{totalBatches} in {batchDuration:F3}s.");

                simsCompleted += (batchEnd - batchStart);
                batchStart = batchEnd;
                NumSimsProgressText.text = $"{simsCompleted} of {numSimulations} Completed";
            }

            DateTime simulationEnd = DateTime.Now;
            double totalDuration = (simulationEnd - simulationStart).TotalSeconds;
            LoggingManager.Log(channelName, $"[Simulation {simNumber}] All batches complete. Total duration: {totalDuration:F3}s.");

            StartCoroutine(EndActions(whiskerSim, simNumber, totalDuration));
        }

        private IEnumerator RunBatch(WhiskerSim whiskerSim, int batchStart, int batchEnd, float duration)
        {
            for (int i = batchStart; i < batchEnd; i++)
            {
                whiskerSim.RunSim(i, duration, false);
                yield return null;
            }
            yield return new WaitUntil(() => whiskerSim.NumberSimsRunning == 0);
        }

        private IEnumerator EndActions(WhiskerSim whiskerSim, int simNumber, double totalDuration)
        {
            Time.timeScale = 1.0f;
            ResultsProcessor.LogSimStateToMonteCarlo(whiskerSim.SimState, simNumber, numSimulations);
            ResultsProcessor.LogMonteCarloResults(simNumber, numSimulations);
            LoggingManager.Log(channelName, $"[Simulation {simNumber}] Completed logging results. Overall run #{simNumber} finished in {totalDuration:F3}s.");
            IsSimulationEnded = true;
            yield return null;
        }
    }
}