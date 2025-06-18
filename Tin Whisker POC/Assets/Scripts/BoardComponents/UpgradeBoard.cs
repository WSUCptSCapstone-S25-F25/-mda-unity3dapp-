using UnityEngine;

namespace BoardComponents
{
    public static class UpgradeBoard
    {
        public static GameObject Upgrade(GameObject boardObj)
        {
            // Attach or ensure Board component
            var boardComp = boardObj.GetComponent<Board>();
            if (!boardComp) boardComp = boardObj.AddComponent<Board>();

            // Ensure Rigidbody
            var rb = boardObj.GetComponent<Rigidbody>();
            if (!rb) rb = boardObj.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            // Ensure a basic collider
            if (!boardObj.GetComponent<Collider>())
            {
                var renderer = boardObj.GetComponentInChildren<Renderer>();
                if (renderer)
                {
                    var box = boardObj.AddComponent<BoxCollider>();
                    box.center = renderer.bounds.center - boardObj.transform.position;
                    box.size = renderer.bounds.size;
                }
                else
                {
                    // Fallback: Add MeshCollider
                    boardObj.AddComponent<MeshCollider>();
                }
            }

            // Set tag for board identification
            boardObj.tag = "Board";

            // Set layer as needed
            boardObj.layer = LayerMask.NameToLayer("Board");

            // Name it clearly
            boardObj.name = "SimBoard";

            return boardObj;
        }
    }
}
