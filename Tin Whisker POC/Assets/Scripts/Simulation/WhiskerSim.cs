using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using System.IO;
using LoggingComponents;
using SimInfo;
using UnityEngine.Serialization;

public class WhiskerSim : MonoBehaviour
{
    private const string channelName = "WhiskerSim";

    public ShortDetector ShortDetector;
    public SimState SimState;
    public GameObject Whisker; // Cylinder/Whisker to clone
    public int NumberSimsRunning;
    public MainController mainController;

    private string myjsonPath;
    public List<GameObject> whiskers = new List<GameObject>();
    private float duration;
    private Coroutine simulationCoroutine;
    private bool render;
    public Shock Shock;
    [FormerlySerializedAs("Shocker")] public ShockController shockController;
    public Vibration Vibration;
    [FormerlySerializedAs("OpenVibration")] public VibrationController vibrationController;

    public void RunSim(int simNumber, float duration, bool render = true)
    {
        LoggingManager.Log(channelName, $"RunSim: Starting simulation #{simNumber} duration={duration} render={render}");
        string layerName = $"Sim layer {simNumber % 10 + 1}";  // For 10 physics layers
        NumberSimsRunning++;
        this.duration = duration;
        this.render = render;

        mainController.ui_lock();

        // Validate if the layer exists
        int layer = LayerMask.NameToLayer(layerName);
        if (layer == -1)
        {
            Debug.LogError($"Layer '{layerName}' does not exist. Please create this layer in the Tags and Layers settings.");
            LoggingManager.Log(channelName, $"RunSim: Aborted because layer '{layerName}' does not exist");
            return;
        }

        SimStateSetUp(simNumber);
        List<WhiskerCollider> whiskerColliders = SpawnWhiskers(layerName);

        if (shockController.shocking)
        {
            LoggingManager.Log(channelName, "RunSim: Starting shock");
            Shock.StartShock();
        }

        if (vibrationController.vibrate)
        {
            LoggingManager.Log(channelName, "RunSim: Starting vibration");
            Vibration.StartVibration();
        }

        ResultsProcessor.LogWhiskers(GetSimLayerWhiskers(whiskers, layerName), simNumber);
        LoggingManager.Log(channelName, $"RunSim: Logged whiskers for simulation #{simNumber}");

        ResultsProcessor.LogSimState(SimState, simNumber);
        LoggingManager.Log(channelName, $"RunSim: Logged SimState for simulation #{simNumber}");

        simulationCoroutine = StartCoroutine(EndSimulationAfterDuration(simNumber));
        ShortDetector.GetComponent<ShortDetector>().StartWhiskerChecks(whiskerColliders, simNumber);
        LoggingManager.Log(channelName, $"RunSim: Detection checks started for simulation #{simNumber}");
    }

    public void ScaleCylinder(GameObject cylinderObject, float widthScale, float heightScale)
    {
        if (cylinderObject == null)
        {
            Debug.LogError("Cylinder object reference is not set.");
            return;
        }

        Vector3 currentScale = cylinderObject.transform.lossyScale;
        float width = currentScale.x * widthScale;
        float height = currentScale.y * heightScale;
        Vector3 newScale = new Vector3(
            width, // Width
            height, // Height
            width  // Depth 
        );
        cylinderObject.transform.localScale = newScale;
        LoggingManager.Log(channelName, $"ScaleCylinder: Scaled cylinder {cylinderObject.name} to {newScale}");
    }

    public void ClearLayerWhiskers(string layerNameToDelete)
    {
        LoggingManager.Log(channelName, $"ClearLayerWhiskers: Clearing whiskers on layer '{layerNameToDelete}'");
        int layerNum = LayerMask.NameToLayer(layerNameToDelete);
        foreach (GameObject whisker in whiskers)
        {
            if (whisker.layer == layerNum)
            {
                DestroyImmediate(whisker);
            }
        }
        whiskers.RemoveAll(whisker => whisker == null);
        LoggingManager.Log(channelName, $"ClearLayerWhiskers: Completed clearing layer '{layerNameToDelete}'");
    }

    public void SaveResults(int simNumber)
    {
        LoggingManager.Log(channelName, $"SaveResults: Saving results for simulation #{simNumber}");
        SimState.SaveSimToJSON(myjsonPath);
        ShortDetector.StopWhiskerChecks(simNumber);
        LoggingManager.Log(channelName, $"SaveResults: Results saved and detection stopped for simulation #{simNumber}");
    }

    private void SimStateSetUp(int simNumber)
    {
        LoggingManager.Log(channelName, $"SimStateSetUp: Preparing state for simulation #{simNumber}");
        myjsonPath = Application.persistentDataPath + "/SimState.JSON";
        if (File.Exists(myjsonPath))
        {
            string jsonString = File.ReadAllText(myjsonPath);
            SimState = JsonUtility.FromJson<SimState>(jsonString);
            SimState.simNumber = simNumber;
            LoggingManager.Log(channelName, $"SimStateSetUp: Loaded existing SimState for simulation #{simNumber}");
        }
        else
        {
            SimState = new SimState();
            SimState.SaveSimToJSON(myjsonPath);
            SimState.simNumber = simNumber;
            LoggingManager.Log(channelName, $"SimStateSetUp: Created new SimState and saved for simulation #{simNumber}");
        }
    }

    private List<WhiskerCollider> SpawnWhiskers(string layerName)
    {
        LoggingManager.Log(channelName, $"SpawnWhiskers: Spawning whiskers on '{layerName}'");
        List<WhiskerCollider> whiskerColliders = new List<WhiskerCollider>();
        Whisker.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 originalScale = Whisker.transform.localScale;

        Vector3 scaledTransform = new Vector3(
            originalScale.x * 10.0f / 1000.0f,
            originalScale.y * 5.0f / 1000.0f,
            originalScale.z * 10.0f / 1000.0f
        );
        Whisker.transform.localScale = scaledTransform;

        float WhiskerCount = SimState.whiskerAmount;
        LognormalRandom lognormalRandomLength = new LognormalRandom(SimState.LengthMu, SimState.LengthSigma);
        LognormalRandom lognormalRandomWidth = new LognormalRandom(SimState.WidthMu, SimState.WidthSigma);

        if (WhiskerCount > 1000)
        {
            WhiskerCount = 1000;
            Debug.LogError("Whisker count is too high. Whisker count: " + WhiskerCount);
            LoggingManager.Log(channelName, $"SpawnWhiskers: Whisker count too high, clamped to {WhiskerCount}");
        }

        int layer = LayerMask.NameToLayer(layerName);
        if (layer == -1)
        {
            Debug.LogError($"Layer '{layerName}' does not exist. Cannot assign layer to whiskers.");
            LoggingManager.Log(channelName, $"SpawnWhiskers: Aborting because layer '{layerName}' not found");
            return whiskerColliders;
        }

        for (int i = 0; i < WhiskerCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-SimState.spawnAreaSizeX / 2f * 10f, SimState.spawnAreaSizeX / 2f * 10f) + SimState.spawnPositionX * 10f - 5f,
                Random.Range(-SimState.spawnAreaSizeY / 2f * 10f, SimState.spawnAreaSizeY / 2f * 10f) + SimState.spawnPositionY * 10f + SimState.spawnAreaSizeY * 10f / 2,
                Random.Range(-SimState.spawnAreaSizeZ / 2f * 10f, SimState.spawnAreaSizeZ / 2f * 10f) + SimState.spawnPositionZ * 10f - 5f
            );
            Quaternion spawnRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

            GameObject newWhisker = Instantiate(Whisker, spawnPosition, spawnRotation);
            newWhisker.layer = layer; // Assign valid layer
            newWhisker.name = $"Whisker{i}";

            newWhisker.GetComponent<MeshRenderer>().enabled = render;
            Collider collider = newWhisker.GetComponent<Collider>();
            if (collider != null)
                collider.enabled = true;

            whiskers.Add(newWhisker);

            float lengthMultiplier = (float)lognormalRandomLength.Next();
            float widthMultiplier = (float)lognormalRandomWidth.Next();

            ScaleCylinder(newWhisker, widthMultiplier, lengthMultiplier);
            WhiskerCollider whiskerCollider = newWhisker.GetComponent<WhiskerCollider>();
            whiskerCollider.WhiskerNum = i;
            whiskerColliders.Add(whiskerCollider);
        }

        LoggingManager.Log(channelName, $"SpawnWhiskers: Created {whiskerColliders.Count} whiskers on layer '{layerName}'");
        return whiskerColliders;
    }

    private void StopShockAndVibrationRoutines()
    {
        if (Shock != null)
        {
            Shock.StopShock(); // Stops the shock logic
            LoggingManager.Log(channelName, "StopShockAndVibrationRoutines: Shock stopped");
        }

        if (Vibration != null)
        {
            Vibration.StopVibration(); // Stops the vibration logic
            LoggingManager.Log(channelName, "StopShockAndVibrationRoutines: Vibration stopped");
        }
    }

    IEnumerator EndSimulationAfterDuration(int simNumber)
    {
        float simulationDuration = duration >= 0.1 ? duration : 10f;
        LoggingManager.Log(channelName, $"EndSimulationAfterDuration: Waiting {simulationDuration} seconds for simulation #{simNumber}");

        yield return new WaitForSeconds(simulationDuration);

        SaveResults(simNumber);
        ClearLayerWhiskers($"Sim layer {simNumber % 10 + 1}");
        StopShockAndVibrationRoutines();
        mainController.ui_unlock();
        LoggingManager.Log(channelName, $"EndSimulationAfterDuration: Simulation #{simNumber} ended and cleaned up");

        foreach (WhiskerCollider whiskerCollider in FindObjectsOfType<WhiskerCollider>())
            whiskerCollider.Cleanup();
        NumberSimsRunning--;
        LoggingManager.Log(channelName, $"EndSimulationAfterDuration: NumberSimsRunning decremented to {NumberSimsRunning}");
        yield return null;
    }

    public void EndSimulationEarly(int simNumber)
    {
        LoggingManager.Log(channelName, $"EndSimulationEarly: Early end requested for simulation #{simNumber}");
        StopCoroutine(simulationCoroutine);
        SaveResults(simNumber);
        ClearLayerWhiskers($"Sim layer {simNumber % 10 + 1}");
        StopShockAndVibrationRoutines();
        mainController.ui_unlock();
        NumberSimsRunning--;
        LoggingManager.Log(channelName, $"EndSimulationEarly: Simulation #{simNumber} ended early, NumberSimsRunning now {NumberSimsRunning}");
    }

    public void InspectMode(int simNumber)
    {
        LoggingManager.Log(channelName, $"InspectMode: Entering inspection mode for simulation #{simNumber}");
        StopCoroutine(simulationCoroutine); // Stop the active simulation coroutine, if any
        StopShockAndVibrationRoutines(); // Stop any shock and vibration routines
        SaveResults(simNumber);
        foreach (GameObject whisker in whiskers)
        {
            Rigidbody rb = whisker.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb); // Remove the rigidbody to stop all physics interactions
            }
        }
        NumberSimsRunning--; // Decrement running simulation count
        LoggingManager.Log(channelName, $"InspectMode: Inspection mode complete, NumberSimsRunning now {NumberSimsRunning}");
    }

    // Function to clear whiskers after inspection
    public void ClearWhiskersAfterInspection()
    {
        LoggingManager.Log(channelName, $"ClearWhiskersAfterInspection: Clearing whiskers after inspection for sim #{SimState.simNumber}");
        ClearLayerWhiskers($"Sim layer {SimState.simNumber % 10 + 1}"); // Clear the whiskers from the layer
        mainController.ui_unlock();
        LoggingManager.Log(channelName, "ClearWhiskersAfterInspection: UI unlocked after inspection");
    }

    private List<GameObject> GetSimLayerWhiskers(List<GameObject> allWhiskers, string layerName)
    {
        List<GameObject> resultWhiskers = new List<GameObject>();
        int layerNum = LayerMask.NameToLayer(layerName);
        foreach (GameObject whisker in allWhiskers)
        {
            if (whisker.layer == layerNum)
            {
                resultWhiskers.Add(whisker);
            }
        }
        return resultWhiskers;
    }
}

