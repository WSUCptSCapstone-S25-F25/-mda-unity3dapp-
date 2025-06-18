using System.Collections;
using System.IO;
using UnityEngine;

namespace Controller
{
    public class SimState
    {
        public int whiskerAmount;
        public float spawnAreaSizeX;
        public float spawnAreaSizeY;
        public float spawnAreaSizeZ;
        public float spawnPositionX;
        public float spawnPositionY;
        public float spawnPositionZ;
        public float LengthMu;
        public float LengthSigma;
        public float WidthMu;
        public float WidthSigma;
        public int simNumber;
        public float simDuration;
        public string objfilePath;
        public string mtlfilePath;
        public bool fileOpened;
        public float vibrationAmplitude;
        public float vibrationSpeed;
        public float ShockIntensity;
        public float ShockDuration;
        public float xTilt; // New variable for x-axis tilt
        public float zTilt; // New variable for z-axis tilt
        public float boardXSize;
        public float boardYSize;
        public float boardZSize;
        public float boardXPos;
        public float boardYPos;
        public float boardZPos;

        public SimState()
        {
            // Default values
            this.whiskerAmount = 10;
            this.spawnAreaSizeX = 2f;
            this.spawnAreaSizeY = 2f;
            this.spawnAreaSizeZ = 2f;
            this.spawnPositionX = 0f;
            this.spawnPositionY = 0f;
            this.spawnPositionZ = 15f;
            this.LengthMu = 0.5f;
            this.LengthSigma = 0.5f;
            this.WidthMu = 0.5f;
            this.WidthSigma = 0.5f;
            this.simNumber = -1;
            this.vibrationAmplitude = 10.0f;
            this.vibrationSpeed = 10.0f;
            this.ShockIntensity = 0.05f;
            this.ShockDuration = 0.025f;
            this.xTilt = 0.0f; // Default x tilt value
            this.zTilt = 0.0f; // Default z tilt value
            this.boardXSize = 1.0f;
            this.boardYSize = 1.0f;
            this.boardZSize = 1.0f;
            this.boardXPos = 0.0f;
            this.boardYPos = 0.0f;
            this.boardYPos = 0.0f;
        }

        public SimState(int whiskerAmount, float spawnAreaSizeX, float spawnAreaSizeY, float spawnAreaSizeZ,
                        float spawnPositionX, float spawnPositionY, float spawnPositionZ, float LengthMu,
                        float LengthSigma, float WidthMu, float WidthSigma, int simNumber, float vibrationAmplitude,
                        float vibrationSpeed, float ShockIntensity, float ShockDuration, float xTilt, float zTilt,
                        float boardXSize, float boardYSize, float boardZSize, float boardXPos, float boardYPos, float boardZPos)
        {
            this.whiskerAmount = whiskerAmount;
            this.spawnAreaSizeX = spawnAreaSizeX;
            this.spawnAreaSizeY = spawnAreaSizeY;
            this.spawnAreaSizeZ = spawnAreaSizeZ;
            this.spawnPositionX = spawnPositionX;
            this.spawnPositionY = spawnPositionY;
            this.spawnPositionZ = spawnPositionZ;
            this.LengthMu = LengthMu;
            this.LengthSigma = LengthSigma;
            this.WidthMu = WidthMu;
            this.WidthSigma = WidthSigma;
            this.simNumber = simNumber;
            this.vibrationAmplitude = vibrationAmplitude;
            this.vibrationSpeed = vibrationSpeed;
            this.ShockIntensity = ShockIntensity;
            this.ShockDuration = ShockDuration;
            this.xTilt = xTilt;
            this.zTilt = zTilt;
            this.boardXSize = boardXSize;
            this.boardYSize = boardYSize;
            this.boardZSize = boardZSize;
            this.boardXPos = boardXPos;
            this.boardYPos = boardYPos;
            this.boardYPos = boardZPos;
        }

        public void SaveSimToJSON(string jsonPath)
        {
            Debug.Log("Attempting to save sim to JSON | Path: " + jsonPath);
            string jsonString = JsonUtility.ToJson(this);
            Debug.Log("Saving -> JSON string:\n" + jsonString);
            File.WriteAllText(jsonPath, jsonString);
        }

        public IEnumerator SaveSimToJSONasync(string jsonPath)
        {
            string jsonString = JsonUtility.ToJson(this);
            var asyncRequest = File.WriteAllTextAsync(jsonPath, jsonString);
            while (!asyncRequest.IsCompleted)
            {
                yield return null;
            }
        }

        public void SaveToCSV(string jsonPath)
        {
            Debug.Log("Attempting to save sim to CSV | Path: " + jsonPath);
            string jsonString = JsonUtility.ToJson(this);
            Debug.Log("Saving -> JSON string:\n" + jsonString);
            File.WriteAllText(jsonPath, jsonString);
        }

        public IEnumerator SaveToCSVasync(string jsonPath)
        {
            string jsonString = JsonUtility.ToJson(this);
            var asyncRequest = File.WriteAllTextAsync(jsonPath, jsonString);
            while (!asyncRequest.IsCompleted)
            {
                yield return null;
            }
        }
    }
}
