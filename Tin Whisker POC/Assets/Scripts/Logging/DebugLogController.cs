using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Logging;

namespace Logging
{
    /// <summary>
    /// Controller for showing/hiding the debug panel and mapping existing Toggle UI elements to LoggerChannels.
    /// All toggles start off unchecked at runtime, with setup warnings if misconfigured.
    /// </summary>
    public class DebugLogController : MonoBehaviour
    {
        [SerializeField] private Button debugToggleButton;
        [SerializeField] private GameObject debugPanel;

        [SerializeField] private List<LoggerChannel> channels;

        [Tooltip("List of Toggle UI components, one per channel")]        
        [SerializeField] private List<Toggle> toggles;

        // Active loggers by channel
        private readonly Dictionary<string, Logger> loggers = new();

        private void Awake()
        {
            // Validate Inspector assignments with warnings
            if (debugToggleButton == null)
            {
                Debug.LogWarning("[DebugLogController] DebugToggleButton is not assigned in the Inspector.");
                enabled = false;
                return;
            }
            if (debugPanel == null)
            {
                Debug.LogWarning("[DebugLogController] DebugPanel is not assigned in the Inspector.");
                enabled = false;
                return;
            }
            if (channels == null || toggles == null)
            {
                Debug.LogWarning("[DebugLogController] Channels and Toggles lists must be assigned in the Inspector.");
                enabled = false;
                return;
            }
            if (channels.Count != toggles.Count)
            {
                Debug.LogWarning("[DebugLogController] Channels.Count must equal Toggles.Count.");
                enabled = false;
                return;
            }

            // Hide panel initially
            debugPanel.SetActive(false);
            debugToggleButton.onClick.AddListener(() => debugPanel.SetActive(!debugPanel.activeSelf));
        }

        private void Start()
        {
            if (!enabled) return;

            // Map each toggle to its channel
            for (int i = 0; i < channels.Count; i++)
            {
                var channel = channels[i];
                var toggle = toggles[i];

                if (channel == null)
                {
                    Debug.LogWarning($"[DebugLogController] channels[{i}] is null.");
                    continue;
                }
                if (toggle == null)
                {
                    Debug.LogWarning($"[DebugLogController] toggles[{i}] is null.");
                    continue;
                }

                // Ensure channel flag off by default
                channel.enabled = false;

                // Initialize toggle unchecked without sending onValueChanged
                toggle.SetIsOnWithoutNotify(false);

                string channelName = channel.channelName;

                toggle.onValueChanged.AddListener(isOn =>
                {
                    if (isOn)
                    {
                        // Create and store logger
                        loggers[channelName] = new Logger(channelName, channelName);
                    }
                    else if (loggers.TryGetValue(channelName, out var logger))
                    {
                        logger.Close();
                        loggers.Remove(channelName);
                    }
                });
            }
        }

        /// <summary>
        /// Logs a message to the specified channel if its toggle is on.
        /// </summary>
        public void Log(string channelName, string message)
        {
            if (loggers.TryGetValue(channelName, out var logger))
                logger.DebugLog(message);
        }
    }
}
