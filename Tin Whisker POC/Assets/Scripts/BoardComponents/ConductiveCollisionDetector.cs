using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoardComponents
{
    public class ConductiveCollisionDetector : MonoBehaviour
    {
        private HashSet<GameObject> currentWhiskers = new();

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Whisker"))
            {
                currentWhiskers.Add(other.gameObject);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Whisker"))
            {
                currentWhiskers.Remove(other.gameObject);
            }
        }

        public HashSet<GameObject> GetCurrentWhiskers() => currentWhiskers;
    }
}