using System.Collections.Generic;
using UnityEngine;

public class LogToggles : MonoBehaviour
{
    public static LogToggles Instance { get; private set; }
    private Dictionary<string, bool> _toggleStates = new Dictionary<string, bool>();

    private void Awake()
    {
        // Standard singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetToggle(string toggleName, bool isOn)
    {
        _toggleStates[toggleName] = isOn;
    }
    
    public bool IsEnabled(string toggleName)
    {
        return _toggleStates.ContainsKey(toggleName) && _toggleStates[toggleName];
    }

    public void DisableAll()
    {
        var keys = new List<string>(_toggleStates.Keys);
        foreach (string key in keys)
            _toggleStates[key] = false;
    }
}
