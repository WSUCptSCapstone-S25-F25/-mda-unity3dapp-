using UnityEngine;
using UnityEngine.UI;

namespace WhiskerComponents
{
    public class WhiskerParameterBinder : MonoBehaviour
    {
        public WhiskerState whiskerState = new WhiskerState();

        public InputField amountField, lengthMuField, lengthSigmaField, widthMuField, widthSigmaField;
        public InputField volumeXField, volumeYField, volumeZField, originXField, originYField, originZField;

        void Awake()
        {
            amountField.onValueChanged.AddListener(s => int.TryParse(s, out whiskerState.Amount));
            lengthMuField.onValueChanged.AddListener(s => float.TryParse(s, out whiskerState.LengthMu));
            lengthSigmaField.onValueChanged.AddListener(s => float.TryParse(s, out whiskerState.LengthSigma));
            widthMuField.onValueChanged.AddListener(s => float.TryParse(s, out whiskerState.WidthMu));
            widthSigmaField.onValueChanged.AddListener(s => float.TryParse(s, out whiskerState.WidthSigma));

            volumeXField.onValueChanged.AddListener(s => whiskerState.SpawnVolumeSize.x = ParseOrZero(s));
            volumeYField.onValueChanged.AddListener(s => whiskerState.SpawnVolumeSize.y = ParseOrZero(s));
            volumeZField.onValueChanged.AddListener(s => whiskerState.SpawnVolumeSize.z = ParseOrZero(s));

            originXField.onValueChanged.AddListener(s => whiskerState.SpawnOrigin.x = ParseOrZero(s));
            originYField.onValueChanged.AddListener(s => whiskerState.SpawnOrigin.y = ParseOrZero(s));
            originZField.onValueChanged.AddListener(s => whiskerState.SpawnOrigin.z = ParseOrZero(s));
        }

        float ParseOrZero(string s)
        {
            float f; return float.TryParse(s, out f) ? f : 0f;
        }
    }
}
