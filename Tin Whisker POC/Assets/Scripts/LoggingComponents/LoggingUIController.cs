using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LoggingComponents
{
    public class LoggingUIController : MonoBehaviour
    {
        [Header("File Logging Controls")]
        [SerializeField] private Toggle enableLogFileToggle;

        [Tooltip("The Input Field where the user types the desired log file name (no extension).")]
        [SerializeField] private TMP_InputField logFileNameInput;

        [Header("Per-Channel Toggles")]
        [SerializeField] private Toggle mainControllerToggle;

        [SerializeField] private Toggle monteCarloToggle;

        [SerializeField] private Toggle whiskerSimToggle;

        private void Awake()
        {
            if (enableLogFileToggle != null)
            {
                enableLogFileToggle.isOn = false;
                enableLogFileToggle.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("[LoggingUIController] enableLogFileToggle is not assigned in the Inspector.");
            }

            if (logFileNameInput != null)
            {
                logFileNameInput.text = "";
                logFileNameInput.onValueChanged.AddListener(OnFilenameValueChanged);
            }
            else
            {
                Debug.LogError("[LoggingUIController] logFileNameInput is not assigned in the Inspector.");
            }

            if (enableLogFileToggle != null)
            {
                enableLogFileToggle.onValueChanged.AddListener(OnFileToggleChanged);
            }

            if (mainControllerToggle != null)
            {
                mainControllerToggle.isOn = false;
                mainControllerToggle.onValueChanged.AddListener(isOn =>
                {
                    LogToggles.Instance.SetToggle("MainController", isOn);
                    Debug.Log($"[LoggingUIController] MainController toggle set to {isOn}");
                });
            }
            else
            {
                Debug.LogError("[LoggingUIController] mainControllerToggle is not assigned in the Inspector.");
            }

            if (monteCarloToggle != null)
            {
                monteCarloToggle.isOn = false;
                monteCarloToggle.onValueChanged.AddListener(isOn =>
                {
                    LogToggles.Instance.SetToggle("MonteCarlo", isOn);
                    Debug.Log($"[LoggingUIController] MonteCarlo toggle set to {isOn}");
                });
            }
            else
            {
                Debug.LogError("[LoggingUIController] monteCarloToggle is not assigned in the Inspector.");
            }
            
            if (whiskerSimToggle != null)
            {
                whiskerSimToggle.isOn = false;
                whiskerSimToggle.onValueChanged.AddListener(isOn =>
                {
                    LogToggles.Instance.SetToggle("WhiskerSim", isOn);
                    Debug.Log($"[LoggingUIController] WhiskerSim toggle set to {isOn}");
                });
            }
            else
            {
                Debug.LogError("[LoggingUIController] WhiskerSimToggle is not assigned in the Inspector.");
            }
        }

        private void OnFilenameValueChanged(string newText)
        {
            if (enableLogFileToggle == null || logFileNameInput == null)
                return;

            bool hasText = !string.IsNullOrWhiteSpace(newText.Trim());
            enableLogFileToggle.gameObject.SetActive(hasText);

            if (!hasText && enableLogFileToggle.isOn)
            {
                string fallback = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
                logFileNameInput.text = fallback;
                LoggingManager.SetFileLogging(true, fallback);
                Debug.Log($"[LoggingUIController] Filename cleared → using fallback '{fallback}.log'");
            }
        }

        private void OnFileToggleChanged(bool isOn)
        {
            if (logFileNameInput == null)
                return;

            if (!isOn)
            {
                LoggingManager.SetFileLogging(false, logFileNameInput.text.Trim());
                return;
            }

            string typedName = logFileNameInput.text.Trim();
            if (string.IsNullOrWhiteSpace(typedName))
            {
                string fallback = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
                logFileNameInput.text = fallback;
                LoggingManager.SetFileLogging(true, fallback);
                Debug.Log($"[LoggingUIController] Empty name → using fallback '{fallback}.log'");
            }
            else
            {
                LoggingManager.SetFileLogging(true, typedName);
                Debug.Log($"[LoggingUIController] Logging to '{typedName}.log'");
            }
        }

        private void OnDestroy()
        {
            LoggingManager.CloseAll();
        }
    }
}
