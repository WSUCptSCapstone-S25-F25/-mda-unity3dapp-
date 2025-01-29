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

    public void GetSimInputs()
    {
        if (int.TryParse(WhiskerAmountText.text, out int result))
        {
            if (result > 1000) // 1000 Max amount of whiskers 
            {
                simState.whiskerAmount = 1000;
            }
            else if (result < 0)
            {
                simState.whiskerAmount = 0;
            }
            else
            {
                simState.whiskerAmount = result;
            }
        }

        if (float.TryParse(LengthMuText.text, out float result2))
        {
            if (result2 > 6.0f)
            {
                simState.LengthMu = 6.0f;
            }
            else if (result2 < 0.1f)
            {
                simState.LengthMu = 0.1f;
            }
            else
            {
                simState.LengthMu = result2;
            }
        }

        if (float.TryParse(LengthSigmaText.text, out float result3))
        {
            if (result3 > 0.3f * result2)
            {
                simState.LengthSigma = 0.3f * result2;
            }
            else if (result3 < 0)
            {
                simState.LengthSigma = 0;
            }
            else
            {
                simState.LengthSigma = result3;
            }
        }

        if (float.TryParse(WidthMuText.text, out float result5))
        {
            if (result5 > 6.0f)
            {
                simState.WidthMu = 6.0f;
            }
            else if (result5 < 0.1f)
            {
                simState.WidthMu = 0.1f;
            }
            else
            {
                simState.WidthMu = result5;
            }
        }

        if (float.TryParse(WidthSigmaText.text, out float result4))
        {
            if (result4 > 0.3f * result3)
            {
                simState.WidthSigma = 0.3f * result3;
            }
            else if (result4 < 0)
            {
                simState.WidthSigma = 0;
            }
            else
            {
                simState.WidthSigma = result4;
            }
        }

        if (float.TryParse(SpawnAreaSizeXText.text, out float result6))
        {
            if (result6 > 300)
            {
                simState.spawnAreaSizeX = 300;
            }
            else if (result6 < 1)
            {
                simState.spawnAreaSizeX = 1.0f;
            }
            else
            {
                simState.spawnAreaSizeX = result6;
            }
        }

        if (float.TryParse(SpawnAreaSizeYText.text, out float result7))
        {
            if (result7 > 300)
            {
                simState.spawnAreaSizeY = 300;
            }
            else if (result7 < 1)
            {
                simState.spawnAreaSizeY = 1;
            }
            else
            {
                simState.spawnAreaSizeY = result7;
            }
        }

        if (float.TryParse(SpawnAreaSizeZText.text, out float result8))
        {
            if (result8 > 300)
            {
                simState.spawnAreaSizeZ = 300;
            }
            else if (result8 < 1)
            {
                simState.spawnAreaSizeZ = 1;
            }
            else
            {
                simState.spawnAreaSizeZ = result8;
            }
        }

        if (float.TryParse(SpawnPositionXText.text, out float result9))
        {
            if (result9 > 300)
            {
                simState.spawnPositionX = 300;
            }
            else if (result9 < -300)
            {
                simState.spawnPositionX = 300;
            }
            else
            {
                simState.spawnPositionX = result9;
            }
        }

        if (float.TryParse(SpawnPositionYText.text, out float result10))
        {
            if (result10 > 300)
            {
                simState.spawnPositionY = 300;
            }
            else if (result10 < -300)
            {
                simState.spawnPositionY = 300;
            }
            else
            {
                simState.spawnPositionY = result10;
            }
        }

        if (float.TryParse(SpawnPositionZText.text, out float result11))
        {
            if (result11 > 300)
            {
                simState.spawnPositionZ = 300;
            }
            else if (result11 < -300)
            {
                simState.spawnPositionZ = 300;
            }
            else
            {
                simState.spawnPositionZ = result11;
            }
        }

        if (float.TryParse(SimDurationText.text, out float result12))
        {
            if (result12 > 20)
            {
                simState.simDuration = 20;
            }
            else if (result12 < 0.1f)
            {
                simState.simDuration = 0.1f;
            }
            else
            {
                simState.simDuration = result12;
            }
        }

        if (int.TryParse(SimQuantityText.text, out int result13))
        {
            if (result13 > 100)
            {
                monteCarloSim.numSimulations = 100;
            }
            else if (result13 <= 0)
            {
                monteCarloSim.numSimulations = 1;
            }
            else
            {
                monteCarloSim.numSimulations = result13;
            }
        }
        if (float.TryParse(VibrationSpeedText.text, out float result14))
            simState.vibrationSpeed = result14;

        if (float.TryParse(VibrationAmplitudeText.text, out float result15))
            simState.vibrationAmplitude = result15;

        if (float.TryParse(ShockIntensityText.text, out float result16))
            simState.ShockIntensity = result16;

        if (float.TryParse(ShockDurationText.text, out float result17))
            simState.ShockDuration = result17;

        if (float.TryParse(xTiltText.text, out float result18))
            simState.xTilt = result18;

        if (float.TryParse(zTiltText.text, out float result19))
            simState.zTilt = result19;

        if (float.TryParse(BoardXSize.text, out float result20))
            simState.boardXSize = result20;

        if (float.TryParse(BoardYSize.text, out float result21))
            simState.boardYSize = result21;
        
        if (float.TryParse(BoardZSize.text, out float result22))
            simState.boardZSize = result22;
        
        if (float.TryParse(BoardXPos.text, out float result23))
            simState.boardXPos = result23;

        if (float.TryParse(BoardYPos.text, out float result24))
            simState.boardYPos = result24;

        if (float.TryParse(BoardZPos.text, out float result25))
            simState.boardZPos = result25;
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

            // TODO: Show object file and mtl file path in results so user knows which PCB was used
            simState.objfilePath = objfilePath;
            simState.mtlfilePath = mtlfilePath;

            // TODO: Make all but end sim button be non-interactable
            GameObject.Find("Run Monte Carlo").GetComponent<Button>().interactable = false;
            GameObject.Find("RunSimButton").GetComponent<Button>().interactable = false;
            MonteCarloWaitScreen.gameObject.SetActive(true);

            simState.SaveSimToJSON(myJsonPath);

            monteCarloSim.RunMonteCarloSim(whiskerSim, SimNumber, simState.simDuration);
            StartCoroutine(EndOfMonteCarloSimActions());  // TODO: Change to end of monte carlo sim actions
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
