using UnityEngine;

namespace Logging
{
    [CreateAssetMenu(menuName = "Logging/LoggerChannel")]
    public class LoggerChannel : ScriptableObject
    {
        public string channelName;
        public bool enabled = true;
    }
}