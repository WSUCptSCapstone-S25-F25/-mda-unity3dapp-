using UnityEngine;

namespace LoggingComponents
{
    public class LogPanelToggler : MonoBehaviour
    {
        [SerializeField] private GameObject loggingPanel;

        private void Awake()
        {
            if (loggingPanel != null)
                loggingPanel.SetActive(false);
        }

        public void TogglePanel()
        {
            if (loggingPanel != null)
                loggingPanel.SetActive(!loggingPanel.activeSelf);
        }
    }
}