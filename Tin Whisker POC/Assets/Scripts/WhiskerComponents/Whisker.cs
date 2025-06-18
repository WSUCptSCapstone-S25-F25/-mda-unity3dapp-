using UnityEngine;

namespace WhiskerComponents
{
    /// <summary>
    /// Data container for a single whisker.
    /// </summary>

    // Collider and Rigidbody reflect rough model of metal whiskers.
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Whisker : MonoBehaviour
    {
        public int Index { get; internal set; }

        private new Collider collider;
        private new Rigidbody rigidbody;

        public void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();

            // Physics only start when enabled.
            rigidbody.isKinematic = true;
            
            // Whisker tag for collision detection.
            gameObject.tag = "Whisker";
        }

        // WhiskerPool calls this method after dispensing this whisker. Starts physics.
        public void Acquire()
        {
            collider.enabled = true;
            rigidbody.isKinematic = false;
        }

        // Called by pool after this whisker is returned - end physics of this whisker.
        public void Release()
        {
            collider.enabled = false;
            rigidbody.isKinematic = true;
        }
    }
}