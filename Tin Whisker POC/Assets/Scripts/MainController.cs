using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LoggingComponents;
// using Logging;
using SimInfo;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public GameObject MonteCarloSimulationObject;
    public int SimNumber = 1; // One start so easier to understand for the average user
    public SimState simState;
    public WhiskerSim whiskerSim;
    private MonteCarloSim monteCarloSim;
    public GameObject ResultsCanvas;
    public TMP_InputField WhiskerAmountText;
    public TMP_InputField LengthSigmaText;
    public TMP_InputField LengthMuText;
    public TMP_InputField WidthSigmaText;
    public TMP_InputField WidthMuText;
    public TMP_InputField SpawnAreaSizeXText;
    public TMP_InputField SpawnAreaSizeYText;
    public TMP_InputField SpawnAreaSizeZText;
    public TMP_InputField SpawnPositionXText;
    public TMP_InputField SpawnPositionYText;
    public TMP_InputField SpawnPositionZText;
    public TMP_InputField SimDurationText;
    public TMP_InputField SimQuantityText;
    public TMP_InputField xTiltText;
    public TMP_InputField zTiltText;
    public TMP_InputField BoardXSize;
    public TMP_InputField BoardYSize;
    public TMP_InputField BoardZSize;
    public TMP_InputField BoardXPos;
    public TMP_InputField BoardYPos;
    public TMP_InputField BoardZPos;
    
    // Constants for mins and maxes:
    // ---- Whisker const
    const int WHISKER_MAX = 1000;
    const int WHISKER_MIN = 0;
    const float MU_MAX = 6.0f;
    const float MU_MIN = 0.1f;
    const float SIGMA_MAX = 0.3f;
    const float SIGMA_MIN = 0f;
    // ---- Spawn Box const
    const float SPAWN_SIZE_MAX_1D = 300f;
    const float SPAWN_SIZE_MIN_1D = 1f;
    const float SPAWN_POSITION_MAX = 300f;
    const float SPAWN_POSITION_MIN = -300f;
    // ---- Simulation const
    const float SIM_DURATION_MAX = 20f;
    const float SIM_DURATION_MIN = 0.1f;
    const int NUM_SIMS_MAX = 10000;
    const int NUM_SIMS_MIN = 1;

    private Button    _showLogButton;
    private GameObject _logPanel;
    
    // ---- Unity UI
    public Button SimulationSettingsButton;
    public Button BoardSettingsButton;

    public Button EndSimEarlyButton;
    public Canvas MonteCarloWaitScreen;
    public Button InspectionModeButton;
    public Button ClearWhiskersButton;

    // ---- PCB files
    public bool PCBloaded = false;
    public string objfilePath;
    public string mtlfilePath;

    // ---- Paths
    private string rootJsonPath;
    private string myJsonPath;

    // ---- Vibration fields
    public TMP_InputField VibrationSpeedText;
    public TMP_InputField VibrationAmplitudeText;
    
    // ---- Shock fields
    public TMP_InputField ShockIntensityText;
    public TMP_InputField ShockDurationText;

    private PopupManager popupManager;

    private string channelName = "MainController";
    
   public void Start()
    {
        LoggingManager.Log(channelName, "Start: Initializing MainController");

        rootJsonPath = Application.persistentDataPath + "/SimState.JSON";
        popupManager = FindObjectOfType<PopupManager>();
        EndSimEarlyButton.gameObject.SetActive(false);
        InspectionModeButton.gameObject.SetActive(false);
        MonteCarloWaitScreen.gameObject.SetActive(false);
        ClearWhiskersButton.gameObject.SetActive(false);

        ui_lock();
        monteCarloSim = MonteCarloSimulationObject.GetComponent<MonteCarloSim>();

        LoggingManager.Log(channelName, "Start: Calling ParameterSetup");
        ParameterSetup();
    }
    public void ShowDebugMessage(string message)
    {
        if (popupManager != null)
        {
            PopupManagerSingleton.Instance.ShowPopup(message);
            LoggingManager.Log(channelName, $"ShowDebugMessage: \"{message}\"");
        }
        else
        {
            Debug.LogError("PopupManager is not assigned. Cannot show popup.");
        }
    }

    private void ParameterSetup()
    {
        LoggingManager.Log(channelName, "ParameterSetup: Loading or creating SimState");

        myJsonPath = rootJsonPath;
        if (System.IO.File.Exists(rootJsonPath))
        {
            string jsonString = System.IO.File.ReadAllText(rootJsonPath);
            simState = JsonUtility.FromJson<SimState>(jsonString);
            simState.simNumber = SimNumber;
            LoggingManager.Log(channelName, "ParameterSetup: Found existing SimState");
        }
        else
        {
            LoggingManager.Log(channelName, "ParameterSetup: No JSON found, creating new SimState");
            simState = new SimState();
            simState.simNumber = SimNumber;
            simState.SaveSimToJSON(rootJsonPath);
            LoggingManager.Log(channelName, $"ParameterSetup: Created new SimState and saved to {rootJsonPath}");
        }

        WhiskerAmountText.text = simState.whiskerAmount.ToString();
        LengthSigmaText.text = simState.LengthSigma.ToString();
        LengthMuText.text = simState.LengthMu.ToString();
        WidthSigmaText.text = simState.WidthSigma.ToString();
        WidthMuText.text = simState.WidthMu.ToString();
        
        SpawnAreaSizeXText.text = simState.spawnAreaSizeX.ToString();
        SpawnAreaSizeYText.text = simState.spawnAreaSizeY.ToString();
        SpawnAreaSizeZText.text = simState.spawnAreaSizeZ.ToString();
        SpawnPositionXText.text = simState.spawnPositionX.ToString();
        SpawnPositionYText.text = simState.spawnPositionY.ToString();
        SpawnPositionZText.text = simState.spawnPositionZ.ToString();
        
        SimDurationText.text = simState.simDuration.ToString();
        
        VibrationSpeedText.text = simState.vibrationSpeed.ToString();
        VibrationAmplitudeText.text = simState.vibrationAmplitude.ToString();
        ShockIntensityText.text = simState.ShockIntensity.ToString();
        ShockDurationText.text = simState.ShockDuration.ToString();
        
        xTiltText.text = simState.xTilt.ToString();
        zTiltText.text = simState.zTilt.ToString();
        
        BoardXPos.text = simState.boardXPos.ToString();
        BoardYPos.text = simState.boardYPos.ToString();
        BoardZPos.text = simState.boardZPos.ToString();
        BoardXSize.text = simState.boardXSize.ToString();
        BoardYSize.text = simState.boardYSize.ToString();
        BoardZSize.text = simState.boardZSize.ToString();

        GetSimInputs();
        simState.SaveSimToJSON(rootJsonPath);
        LoggingManager.Log(channelName, "ParameterSetup: Saved SimState and setting up spawn box");
        SetUpSpawnBox();
    }

    private void SetUpSpawnBox()
    {
        GameObject spawnBoxObject = GameObject.Find("WhiskerSpawnBox");
        if (spawnBoxObject != null)
        {
            SpawnBoxController spawnBoxController = spawnBoxObject.GetComponent<SpawnBoxController>();
            if (spawnBoxController != null)
            {
                spawnBoxController.UpdateCubeProperties();
                LoggingManager.Log(channelName, "SetUpSpawnBox: Updated spawn box properties");
            }
            else
            {
                Debug.LogError("SpawnBoxController component not found on GameObject 'WhiskerSpawnBox'.");
            }
        }
        else
        {
            Debug.LogError("GameObject 'WhiskerSpawnBox' not found in the scene.");
        }
    }

    private float ParseAndClampFloat(string text, float min, float max)
    {
        if (float.TryParse(text, out var v))
            return Mathf.Clamp(v, min, max);
        return min;
    }

    private int ParseAndClampInt(string text, int min, int max)
    {
        if (int.TryParse(text, out var v))
            return Mathf.Clamp(v, min, max);
        return min;
    }

    public void GetSimInputs()
    {
        LoggingManager.Log(channelName, "GetSimInputs: Reading and clamping user input");
        simState.whiskerAmount = ParseAndClampInt(WhiskerAmountText.text, WHISKER_MIN, WHISKER_MAX);
        simState.LengthMu = ParseAndClampFloat(LengthMuText.text, MU_MIN, MU_MAX);
        simState.LengthSigma = ParseAndClampFloat(LengthSigmaText.text, SIGMA_MIN, SIGMA_MAX * simState.LengthMu);
        simState.WidthMu = ParseAndClampFloat(WidthMuText.text, MU_MIN, MU_MAX);
        simState.WidthSigma = ParseAndClampFloat(WidthSigmaText.text, SIGMA_MIN, SIGMA_MAX * simState.WidthMu);
        simState.spawnAreaSizeX = ParseAndClampFloat(SpawnAreaSizeXText.text, SPAWN_SIZE_MIN_1D, SPAWN_SIZE_MAX_1D);
        simState.spawnAreaSizeY = ParseAndClampFloat(SpawnAreaSizeYText.text, SPAWN_SIZE_MIN_1D, SPAWN_SIZE_MAX_1D);
        simState.spawnAreaSizeZ = ParseAndClampFloat(SpawnAreaSizeZText.text, SPAWN_SIZE_MIN_1D, SPAWN_SIZE_MAX_1D);
        simState.spawnPositionX = ParseAndClampFloat(SpawnPositionXText.text, SPAWN_POSITION_MIN, SPAWN_POSITION_MAX);
        simState.spawnPositionY = ParseAndClampFloat(SpawnPositionYText.text, SPAWN_POSITION_MIN, SPAWN_POSITION_MAX);
        simState.spawnPositionZ = ParseAndClampFloat(SpawnPositionZText.text, SPAWN_POSITION_MIN, SPAWN_POSITION_MAX);
        simState.simDuration = ParseAndClampFloat(SimDurationText.text, SIM_DURATION_MIN, SIM_DURATION_MAX);

        monteCarloSim.numSimulations = ParseAndClampInt(SimQuantityText.text, NUM_SIMS_MIN, NUM_SIMS_MAX);

        if (float.TryParse(VibrationSpeedText.text, out float vibSpeed)) simState.vibrationSpeed = vibSpeed;
        if (float.TryParse(VibrationAmplitudeText.text, out float vibAmp)) simState.vibrationAmplitude = vibAmp;
        if (float.TryParse(ShockIntensityText.text, out float shockInt)) simState.ShockIntensity = shockInt;
        if (float.TryParse(ShockDurationText.text, out float shockDur)) simState.ShockDuration = shockDur;

        if (float.TryParse(BoardXSize.text, out float bx)) simState.boardXSize = bx;
        if (float.TryParse(BoardYSize.text, out float by)) simState.boardYSize = by;
        if (float.TryParse(BoardZSize.text, out float bz)) simState.boardZSize = bz;

        simState.boardXPos = ParseAndClampFloat(BoardXPos.text, SPAWN_POSITION_MIN, SPAWN_POSITION_MAX);
        simState.boardYPos = ParseAndClampFloat(BoardYPos.text, SPAWN_POSITION_MIN, SPAWN_POSITION_MAX);
        simState.boardZPos = ParseAndClampFloat(BoardZPos.text, SPAWN_POSITION_MIN, SPAWN_POSITION_MAX);
    }

    public void ui_lock()
    {
        LoggingManager.Log(channelName, "ui_lock: Disabling UI buttons");
        SimulationSettingsButton.interactable = false;
        BoardSettingsButton.interactable = false;
    }

    public void ui_unlock()
    {
        LoggingManager.Log(channelName, "ui_unlock: Enabling UI buttons");
        SimulationSettingsButton.interactable = true;
        BoardSettingsButton.interactable = true;
    }

    public void RunSimulation()
    {
        LoggingManager.Log(channelName, "RunSimulation: Attempting to start simulation");
        if (PCBloaded)
        {
            ShowDebugMessage("Simulation starting.");
            simState.simNumber = SimNumber;
            LoggingManager.Log(channelName, $"RunSimulation: SimNumber set to {SimNumber}");

            GetSimInputs();
            simState.objfilePath = objfilePath;
            simState.mtlfilePath = mtlfilePath;

            LoggingManager.Log(channelName, "RunSimulation: Saving simState to JSON");
            simState.SaveSimToJSON(myJsonPath);

            LoggingManager.Log(channelName, $"RunSimulation: Starting whiskerSim with duration {simState.simDuration}");
            whiskerSim.RunSim(SimNumber, simState.simDuration);
            StartCoroutine(EndOfSimActions());
        }
        else
        {
            ShowDebugMessage("No loaded PCB");
            LoggingManager.Log(channelName, "RunSimulation: Aborted due to no PCB loaded");
        }
    }

    IEnumerator EndOfSimActions()
    {
        yield return new WaitUntil(() => whiskerSim.NumberSimsRunning == 0);

        ShowDebugMessage("Simulation ended.");
        LoggingManager.Log(channelName, "EndOfSimActions: Simulation completed");

        GameObject.Find("Sim results").GetComponent<Button>().interactable = true;
        GameObject.Find("RunSimButton").GetComponent<Button>().interactable = true;
        EndSimEarlyButton.gameObject.SetActive(false);
        InspectionModeButton.gameObject.SetActive(false);
        SimNumber++;
        LoggingManager.Log(channelName, $"EndOfSimActions: Incremented SimNumber to {SimNumber}");
    }

    public void EndSimulationEarly()
    {
        ShowDebugMessage("User interrupt.");
        LoggingManager.Log(channelName, "EndSimulationEarly: User requested early end");
        whiskerSim.EndSimulationEarly(SimNumber);
    }

    public void InspectionModeUI()
    {
        ShowDebugMessage("User Inspection Mode.");
        LoggingManager.Log(channelName, "InspectionModeUI: Entering inspection mode");
        InspectionModeButton.gameObject.SetActive(false);
        ClearWhiskersButton.gameObject.SetActive(true);
        whiskerSim.InspectMode(SimNumber);
    }

    public void ClearWhiskers()
    {
        whiskerSim.ClearWhiskersAfterInspection();
        LoggingManager.Log(channelName, "ClearWhiskers: Cleared whiskers after inspection");
        ClearWhiskersButton.gameObject.SetActive(false);
    }

    public void RunMonteCarloSimulation()
    {
        LoggingManager.Log(channelName, "RunMonteCarloSimulation: Attempting Monte Carlo run");
        if (PCBloaded)
        {
            simState.simNumber = SimNumber;
            LoggingManager.Log(channelName, $"RunMonteCarloSimulation: SimNumber set to {SimNumber}");
            GetSimInputs();

            simState.objfilePath = objfilePath;
            simState.mtlfilePath = mtlfilePath;

            GameObject.Find("Run Monte Carlo").GetComponent<Button>().interactable = false;
            GameObject.Find("RunSimButton").GetComponent<Button>().interactable = false;
            MonteCarloWaitScreen.gameObject.SetActive(true);

            LoggingManager.Log(channelName, "RunMonteCarloSimulation: Saving simState to JSON");
            simState.SaveSimToJSON(myJsonPath);

            LoggingManager.Log(channelName, "RunMonteCarloSimulation: Calling MonteCarloSim");
            monteCarloSim.RunMonteCarloSim(whiskerSim, SimNumber, simState.simDuration);
            StartCoroutine(EndOfMonteCarloSimActions());
        }
        else
        {
            ShowDebugMessage("No loaded PCB");
            LoggingManager.Log(channelName, "RunMonteCarloSimulation: Aborted due to no PCB loaded");
        }
    }

    IEnumerator EndOfMonteCarloSimActions()
    {
        yield return new WaitUntil(() => monteCarloSim.IsSimulationEnded);
        ShowDebugMessage("Monte Carlo simulation ended.");
        LoggingManager.Log(channelName, "EndOfMonteCarloSimActions: Monte Carlo completed");

        GameObject.Find("Run Monte Carlo").GetComponent<Button>().interactable = true;
        GameObject.Find("RunSimButton").GetComponent<Button>().interactable = true;
        MonteCarloWaitScreen.gameObject.SetActive(false);
        SimNumber += monteCarloSim.numSimulations;
        LoggingManager.Log(channelName, $"EndOfMonteCarloSimActions: SimNumber updated to {SimNumber}");
    }

    public void SwitchToResults()
    {
        LoggingManager.Log(channelName, "SwitchToResults: Switching to results canvas");
        if (ResultsCanvas != null)
            ResultsCanvas.SetActive(true);
        GameObject.Find("MainCanvas").SetActive(false);
    }

    public void QuitApplication()
    {
        LoggingManager.Log(channelName, "QuitApplication: Quitting application");
        Debug.Log("Quitting application");
        Debug.Log("Sim number: " + simState.simNumber);
        // Quit the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Application.Quit();
    }
}