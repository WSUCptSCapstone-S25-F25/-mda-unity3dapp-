using System;
using UnityEngine;
using System.Collections;
using Logging;
using TMPro;

// All physics occur on Unity main thread. Cannot multi-thread processes.

// TODO: Run batches on simultaneous scenes.
public class MonteCarloSim : MonoBehaviour
{
    // Logging strings --------------------------------
    private const string channelName = "MonteCarloSim";
    
    private static readonly string RunMonteCarloMessage = "--------------------- Started Monte Carlo Instance ---------------------";
    private static readonly string MonteCarloSimEnd = "Monte Carlo Sim Ended ------------------------------------------";
    
    private static readonly string RunBatchMessage = "------------ Starting batch";
    private static readonly string EndBatchMessage = "------------ Ended batch";
    
    // ---------------------------------------------
    public int numSimulations = 2; // 2 Default
    public TextMeshProUGUI NumSimsProgressText;
    public bool IsSimulationEnded;

    private const int MAX_BATCH_SIZE = 10;
    
    // [SerializeField] private DebugLogController debugLogController;
    
    // Awake waits for Unity to finish initializing resources
    
    public void RunMonteCarloSim(WhiskerSim whiskerSim, int simNumber, float duration)
    {
        var startTime = DateTime.Now;
        // debugLogController.Log(channelName, RunMonteCarloMessage);
            
        IsSimulationEnded = false;
        Time.timeScale = 10.0f;
        
        StartCoroutine(RunBatches(whiskerSim, simNumber, duration));
        
        var endTime = DateTime.Now;
        string endMonteCarloMessage = 
            LoggingUtility.MakeMessageTimeDiff(startTime, endTime, MonteCarloSimEnd);
        
        // logger.Log(endMonteCarloMessage);
        // logger.Log($"Status of logger toggle: {logger.GetEnabled()}");
    }


        private IEnumerator RunBatches(WhiskerSim whiskerSim, int beginningSimNumber, float duration)
    {
        int totalSimulations = numSimulations;
        int batchStart = beginningSimNumber;
        int simsRun = 0;
        int currentBatchCount = 0;
        int totalBatches = totalSimulations / MAX_BATCH_SIZE;
        if(totalSimulations % MAX_BATCH_SIZE != 0)
            totalBatches++;
        
        NumSimsProgressText.text = $"{simsRun} of {numSimulations} Completed";
        while (batchStart < beginningSimNumber + totalSimulations)
        {
            currentBatchCount++;
            int batchEnd = Mathf.Min(batchStart + MAX_BATCH_SIZE + beginningSimNumber, totalSimulations + beginningSimNumber);

            // logger.Log(RunBatchMessage + $" {currentBatchCount} of {totalBatches}");
            
            DateTime batchStartTime = DateTime.Now;
            yield return RunBatch(whiskerSim, batchStart, batchEnd, duration);
            DateTime batchEndTime = DateTime.Now;

            string log = LoggingUtility.MakeMessageTimeDiff(batchStartTime, batchEndTime, EndBatchMessage);
            // logger.Log(log);

            simsRun += batchEnd - batchStart;
            
            batchStart = batchEnd;
            NumSimsProgressText.text = $"{simsRun} of {numSimulations} Completed";
        }

        StartCoroutine(EndActions(whiskerSim, beginningSimNumber));
    }

    private IEnumerator RunBatch(WhiskerSim whiskerSim, int batchStart, int batchEnd, float duration)
    {
        // Debug.Log($"Running simulations from {batchStart} to {batchEnd - 1}");
        for (int i = batchStart; i < batchEnd; i++)
        {
            whiskerSim.RunSim(i, duration, false);
            
            // Yield return null allows Unity to update the UI and restart physics.
            yield return null;
        }
        yield return new WaitUntil(() => whiskerSim.NumberSimsRunning == 0);
    }

    private IEnumerator EndActions(WhiskerSim whiskerSim, int beginningSimNumber)
    {
        // Debug.Log("End of monte carlo sim");
        Time.timeScale = 1.0f;
        ResultsProcessor.LogSimStateToMonteCarlo(whiskerSim.SimState, beginningSimNumber, numSimulations);
        ResultsProcessor.LogMonteCarloResults(beginningSimNumber, numSimulations);
        IsSimulationEnded = true;
        yield return null;
    }
}