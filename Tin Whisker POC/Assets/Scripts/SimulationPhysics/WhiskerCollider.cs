using System.Collections.Generic;
using UnityEngine;

namespace SimulationPhysics
{
    public class WhiskerCollider : MonoBehaviour
    {
        public int WhiskerNum = 0;
        private HashSet<GameObject> currentlyCollidingObjects = new HashSet<GameObject>();

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Conductive"))
            {
                currentlyCollidingObjects.Add(collision.gameObject);
                // Renderer objectRenderer = GetComponent<Renderer>();
                // objectRenderer.material.color = Color.red;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Conductive"))
            {
                currentlyCollidingObjects.Remove(collision.gameObject);
                // Renderer objectRenderer = GetComponent<Renderer>();
                // objectRenderer.material.color = Color.white;
            }
        }

        public bool IsBridgingComponents()
        {
            return currentlyCollidingObjects.Count > 1;
        }

        public GameObject[] GetBridgedComponents()
        {
            // HashSet to List
            List<GameObject> bridgedComponents = new List<GameObject>(currentlyCollidingObjects);
            // List to Array
            return bridgedComponents.ToArray();
        }

        // Cleanup method to be called when clearing the scene or resetting the collider
        public void Cleanup()
        {
            currentlyCollidingObjects.Clear();
        }

        // Optionally, you could call Cleanup in OnDestroy if you want it to automatically clean up when destroyed
        private void OnDestroy()
        {
            Cleanup();
        }
    }
}


