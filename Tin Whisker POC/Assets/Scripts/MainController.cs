using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    public Button SimulationSettingsButton;
    public Button BoardSettingsButton;

    public Button EndSimEarlyButton;
    public Canvas MonteCarloWaitScreen;
    public Button InspectionModeButton;
    public Button ClearWhiskersButton;

    public bool PCBloaded = false;
    public string objfilePath;
    public string mtlfilePath;

    private string rootJsonPath;
    private string myJsonPath;

    public TMP_InputField VibrationSpeedText;
    public TMP_InputField VibrationAmplitudeText;
    public TMP_InputField ShockIntensityText;
    public TMP_InputField ShockDurationText;

    private PopupManager popupManager;

    public void Start()
    {
        rootJsonPath = Application.persistentDataPath + "/SimState.JSON";
        popupManager = FindObjectOfType<PopupManager>();
        EndSimEarlyButton.gameObject.SetActive(false);
        InspectionModeButton.gameObject.SetActive(false);
        MonteCarloWaitScreen.gameObject.SetActive(false);
        ClearWhiskersButton.gameObject.SetActive(false);
        ui_lock();
        monteCarloSim = MonteCarloSimulationObject.GetComponent<MonteCarloSim>();

        ParameterSetup();
    }

    public void ShowDebugMessage(string message)
    {
        if (popupManager != null)
        {
            PopupManagerSingleton.Instance.ShowPopup(message);
        }
        else
        {
            Debug.LogError("PopupManager is not assigned. Cannot show popup.");
        }
    }
   
    private void ParameterSetup()
    {
        myJsonPath = rootJsonPath;
        if (System.IO.File.Exists(rootJsonPath))
        {
            // JSON folder exists, read data from file and initialize SimState object
            string jsonString = System.IO.File.ReadAllText(rootJsonPath);
            simState = JsonUtility.FromJson<SimState>(jsonString);
            simState.simNumber = SimNumber;
        }
        else
        {
            // JSON folder doesn't exist, create SimState object with default constructor
            Debug.Log("root JSON not found\nSaving class to JSON");
            simState = new SimState();
            simState.simNumber = SimNumber;
            if (simState == null)
            {
                ShowDebugMessage("No sim state found");
            }
            else
            {
                Debug.Log("Sim state found");
                Debug.Log("rootJsonPath: " + rootJsonPath);
            }
            simState.SaveSimToJSON(rootJsonPath);
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

        // Get the float value from the text field
        GetSimInputs();
        simState.SaveSimToJSON(rootJsonPath);
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
                // Call UpdateCubeProperties on the SpawnBoxController instance
                spawnBoxController.UpdateCubeProperties();
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
        // default to lower bound
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
        // Whisker parameters
        // ----------------------- Total Whiskers -------------------------------------
        simState.whiskerAmount = ParseAndClampInt(
            WhiskerAmountText.text,
            WHISKER_MIN,
            WHISKER_MAX
        );
        // ----------------------- lognormal length of whiskers. -------------------------------------
        simState.LengthMu = ParseAndClampFloat(
            LengthMuText.text,
            MU_MIN,
            MU_MAX
        );
        simState.LengthSigma = ParseAndClampFloat(
            LengthSigmaText.text,
            SIGMA_MIN,
            SIGMA_MAX * simState.LengthMu
        );
        
        // ------------------------- lognormal diameters of whiskers ----------------------------------
        simState.WidthMu = ParseAndClampFloat(
            WidthMuText.text,
            MU_MIN,
            MU_MAX
        );
        simState.WidthSigma = ParseAndClampFloat(
            WidthSigmaText.text,
            SIGMA_MIN,
            SIGMA_MAX * simState.WidthMu
        );
        
        // Spawn box parameters.
        // ---------------- Box Size ----------------------------------
        simState.spawnAreaSizeX = ParseAndClampFloat(
            SpawnAreaSizeXText.text,
            SPAWN_SIZE_MIN_1D,
            SPAWN_SIZE_MAX_1D
        );
        simState.spawnAreaSizeY = ParseAndClampFloat(
            SpawnAreaSizeYText.text,
            SPAWN_SIZE_MIN_1D,
            SPAWN_SIZE_MAX_1D
        );
        simState.spawnAreaSizeZ = ParseAndClampFloat(
            SpawnAreaSizeZText.text,
            SPAWN_SIZE_MIN_1D,
            SPAWN_SIZE_MAX_1D
        );

        // ----------------------- Box Origin -------------------------------------
        simState.spawnPositionX = ParseAndClampFloat(
            SpawnPositionXText.text,
            SPAWN_POSITION_MIN,
            SPAWN_POSITION_MAX
        );
        simState.spawnPositionY = ParseAndClampFloat(
            SpawnPositionYText.text,
            SPAWN_POSITION_MIN,
            SPAWN_POSITION_MAX
        );
        simState.spawnPositionZ = ParseAndClampFloat(
            SpawnPositionZText.text,
            SPAWN_POSITION_MIN,
            SPAWN_POSITION_MAX
        );

        // ----------------------- Simulation duration -------------------------------------
        simState.simDuration = ParseAndClampFloat(
            SimDurationText.text,
            SIM_DURATION_MIN,
            SIM_DURATION_MAX
        );

        // ----------------------- Total simulations for a monte-carlo instance  -------------------------------------
        monteCarloSim.numSimulations = ParseAndClampInt(
            SimQuantityText.text,
            NUM_SIMS_MIN,
            NUM_SIMS_MAX
        );
        
        // Mechanical vibrations
        // ----------------------- Vibration speed  -------------------------------------
        if (float.TryParse(VibrationSpeedText.text, out float result14))
            simState.vibrationSpeed = result14;

        // ----------------------- Vibration amplitude -------------------------------------
        if (float.TryParse(VibrationAmplitudeText.text, out float result15))
            simState.vibrationAmplitude = result15;

        // ----------------------- Shock intensity -------------------------------------
        if (float.TryParse(ShockIntensityText.text, out float result16))
            simState.ShockIntensity = result16;

        // ----------------------- Shock duration -------------------------------------
        if (float.TryParse(ShockDurationText.text, out float result17))
            simState.ShockDuration = result17;

        // Board params
        // ---- Dimensions of board (temporary imports – no clamps)
        if (float.TryParse(BoardXSize.text, out float bx))   simState.boardXSize = bx;
        if (float.TryParse(BoardYSize.text, out float by))   simState.boardYSize = by;
        if (float.TryParse(BoardZSize.text, out float bz))   simState.boardZSize = bz;

        // ---- Board positioning from origin (clamped same as spawn position)
        simState.boardXPos = ParseAndClampFloat(
            BoardXPos.text,
            SPAWN_POSITION_MIN,
            SPAWN_POSITION_MAX
        );
        simState.boardYPos = ParseAndClampFloat(
            BoardYPos.text,
            SPAWN_POSITION_MIN,
            SPAWN_POSITION_MAX
        );
        simState.boardZPos = ParseAndClampFloat(
            BoardZPos.text,
            SPAWN_POSITION_MIN,
            SPAWN_POSITION_MAX
        );
    }

    public void ui_lock()
    {
        SimulationSettingsButton.interactable = false;
        BoardSettingsButton.interactable = false;
    }

    public void ui_unlock()
    {
        SimulationSettingsButton.interactable = true;
        BoardSettingsButton.interactable = true;
    }

    public void RunSimulation()
    {
        if (PCBloaded)
        {
            ShowDebugMessage("Simulation starting. ");
            simState.simNumber = SimNumber;
            Debug.Log("Sim num: " + SimNumber);
            GetSimInputs();

            // TODO: Show object file and mtl file path in results so user knows which PCB was used
            simState.objfilePath = objfilePath;
            simState.mtlfilePath = mtlfilePath;

            // TODO: Make all but end sim button be non-interactable
            GameObject.Find("RunSimButton").GetComponent<Button>().interactable = false;
            GameObject.Find("Sim results").GetComponent<Button>().interactable = false;
            EndSimEarlyButton.gameObject.SetActive(true);
            InspectionModeButton.gameObject.SetActive(true);

            simState.SaveSimToJSON(myJsonPath);

            whiskerSim.RunSim(SimNumber, simState.simDuration);
            StartCoroutine(EndOfSimActions());
        }
        else
        {
            ShowDebugMessage("No loaded PCB");
        }
    }

    IEnumerator EndOfSimActions()
    {
        yield return new WaitUntil(() => whiskerSim.NumberSimsRunning == 0);

        ShowDebugMessage("Simulation ended.");
        GameObject.Find("Sim results").GetComponent<Button>().interactable = true;
        GameObject.Find("RunSimButton").GetComponent<Button>().interactable = true;
        EndSimEarlyButton.gameObject.SetActive(false);
        InspectionModeButton.gameObject.SetActive(false);
        SimNumber++;
    }

    public void EndSimulationEarly()
    {
        ShowDebugMessage("User interupt. ");
        whiskerSim.EndSimulationEarly(SimNumber);
    }

    public void InspectionModeUI()
    {
        ShowDebugMessage("User Inspection Mode. ");
        InspectionModeButton.gameObject.SetActive(false);
        ClearWhiskersButton.gameObject.SetActive(true);
        whiskerSim.InspectMode(SimNumber);

    }

    public void ClearWhiskers()
    {
        whiskerSim.ClearWhiskersAfterInspection();
        ClearWhiskersButton.gameObject.SetActive(false);
    }

    public void RunMonteCarloSimulation()
    {
        if (PCBloaded)
        {
            simState.simNumber = SimNumber;
            Debug.Log("Sim num: " + SimNumber);
            GetSimInputs();

            simState.objfilePath = objfilePath;
            simState.mtlfilePath = mtlfilePath;

            GameObject.Find("Run Monte Carlo").GetComponent<Button>().interactable = false;
            GameObject.Find("RunSimButton").GetComponent<Button>().interactable = false;
            MonteCarloWaitScreen.gameObject.SetActive(true);

            simState.SaveSimToJSON(myJsonPath);

            monteCarloSim.RunMonteCarloSim(whiskerSim, SimNumber, simState.simDuration);
            StartCoroutine(EndOfMonteCarloSimActions());
        }
        else
        {
            ShowDebugMessage("No loaded PCB");
        }
    }

    IEnumerator EndOfMonteCarloSimActions()
    {
        yield return new WaitUntil(() => monteCarloSim.IsSimulationEnded);

        ShowDebugMessage("Monte Carlo simulation ended.");
        GameObject.Find("Run Monte Carlo").GetComponent<Button>().interactable = true;
        GameObject.Find("RunSimButton").GetComponent<Button>().interactable = true;
        MonteCarloWaitScreen.gameObject.SetActive(false);
        SimNumber += monteCarloSim.numSimulations;

    }


    public void SwitchToResults()
    {
        if (ResultsCanvas != null)
            ResultsCanvas.SetActive(true);
        GameObject.Find("MainCanvas").SetActive(false);
    }

    public void QuitApplication()
    {
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
