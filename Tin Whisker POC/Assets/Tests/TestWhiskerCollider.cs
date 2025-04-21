using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;

namespace TinWhiskerPOC
{
    public class TestWhiskerCollider
    {
        /// <summary>
        /// testing IsBridgingComponent() normal case, there are two conductive components
        /// </summary>
        [Test]
        public void TestIsBridgingComponentNormalCase()
        {
            // creates an instance of a WhiskerCollider and two conductive materials
            WhiskerCollider wCollider = new GameObject().AddComponent<WhiskerCollider>();
            GameObject conductiveMaterial1 = new GameObject("conductiveMaterial1");
            GameObject conductiveMaterial2 = new GameObject("conductiveMaterial2");
            conductiveMaterial1.tag = "Conductive";
            conductiveMaterial2.tag = "Conductive";

            // gets private field 
            var field = typeof(WhiskerCollider).GetField("currentlyCollidingObjects", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            
            // adds materials to the hashset
            field.SetValue(wCollider, new HashSet<GameObject> {conductiveMaterial1, conductiveMaterial2});

            Assert.AreEqual(true, wCollider.IsBridgingComponents());
        }

        /// <summary>
        /// testing IsBridgingComponent() edge case, there is only one conductive components
        /// </summary>
        [Test]
        public void TestIsBridgingComponentEdgeCase()
        {
            // creates an instance of a WhiskerCollider and one conductive material
            WhiskerCollider wCollider = new GameObject().AddComponent<WhiskerCollider>();
            GameObject conductiveMaterial1 = new GameObject("conductiveMaterial1");
            conductiveMaterial1.tag = "Conductive";

            // gets private field 
            var field = typeof(WhiskerCollider).GetField("currentlyCollidingObjects", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            
            // adds material to the hashset
            field.SetValue(wCollider, new HashSet<GameObject> {conductiveMaterial1});

            Assert.AreEqual(false, wCollider.IsBridgingComponents());
        }

        /// <summary>
        /// testing IsBridgingComponent() exceptional case, WhiskerCollider has no conductive materials
        /// </summary>
        [Test]
        public void TestIsBridgingComponentExceptionalCase()
        {
            // creates an instance of a WhiskerCollider
            WhiskerCollider wCollider = new GameObject().AddComponent<WhiskerCollider>();

            // gets private field 
            // var field = typeof(WhiskerCollider).GetField("currentlyCollidingObjects", 
            //     BindingFlags.NonPublic | BindingFlags.Instance);
            
            // // adds material to the hashset
            // field.SetValue(wCollider, new HashSet<GameObject> {conductiveMaterial1});

            Assert.AreEqual(false, wCollider.IsBridgingComponents());
        }
        
        /// <summary>
        /// testing GetBridgedComponent() normal case, there are two conductive components
        /// </summary>
        [Test]
        public void TestGetBridgedComponentNormalCase()
        {
            // creates an instance of a WhiskerCollider and two conductive materials
            WhiskerCollider wCollider = new GameObject().AddComponent<WhiskerCollider>();
            GameObject conductiveMaterial1 = new GameObject("conductiveMaterial1");
            GameObject conductiveMaterial2 = new GameObject("conductiveMaterial2");
            conductiveMaterial1.tag = "Conductive";
            conductiveMaterial2.tag = "Conductive";

            // gets private field 
            var field = typeof(WhiskerCollider).GetField("currentlyCollidingObjects", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            
            // adds materials to the hashset
            field.SetValue(wCollider, new HashSet<GameObject> {conductiveMaterial1, conductiveMaterial2});

            Assert.AreEqual(conductiveMaterial1, wCollider.GetBridgedComponents()[0]);
            Assert.AreEqual(conductiveMaterial2, wCollider.GetBridgedComponents()[1]);

        }

        /// <summary>
        /// testing GetBridgedComponent() edge case, there is no conductive components
        /// </summary>
        [Test]
        public void TestGetBridgedComponentEdgeCase()
        {
            // creates an instance of a WhiskerCollider
            WhiskerCollider wCollider = new GameObject().AddComponent<WhiskerCollider>();

            Assert.AreEqual(0, wCollider.GetBridgedComponents().Length);
        }

        /// <summary>
        /// testing GetBridgedComponent() exceptional case, WhiskerCollider has no conductive materials
        /// </summary>
        // [Test]
        // public void TestGetBridgedComponentExceptionalCase()
        // {
        //     // creates an instance of a WhiskerCollider
        //     WhiskerCollider wCollider = new GameObject().AddComponent<WhiskerCollider>();

        //     Assert.AreEqual(false, wCollider.IsBridgingComponents());
        // }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestWhiskerColliderWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
