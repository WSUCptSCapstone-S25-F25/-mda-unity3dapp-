using System;
using FileBrowserComponents;
using UnityEngine;

namespace BoardComponents
{
    public static class BoardLoader
    {
        public static void LoadBoard(string relativePath, Action<GameObject> onBoardLoaded)
        {
            FileManager.OpenFilePanel(relativePath, new[] { "obj" }, (fullPath) =>
            {
                if (string.IsNullOrEmpty(fullPath))
                {
                    Debug.LogWarning("Board file not selected.");
                    onBoardLoaded?.Invoke(null);
                    return;
                }
            
                var loadedObj = new Dummiesman.OBJLoader().Load(fullPath); 

                if (loadedObj == null)
                {
                    Debug.LogError("Board import failed.");
                    onBoardLoaded?.Invoke(null);
                    return;
                }

                onBoardLoaded?.Invoke(loadedObj);
            });
        }
    }
}

