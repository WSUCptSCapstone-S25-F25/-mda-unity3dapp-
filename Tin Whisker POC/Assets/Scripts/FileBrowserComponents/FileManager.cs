using System;
using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace FileBrowserComponents
{
    public static class FileManager
    {
        public static void OpenFilePanel(string relativePath, string[] extensions, Action<string> onSelected)
        {
            string rootPath = Application.dataPath; // Unity's Assets folder (full path)
            string startPath = Path.Combine(rootPath, relativePath);

#if UNITY_EDITOR
            // In Editor, use Unity's dialog
            string ext = extensions != null && extensions.Length > 0 ? extensions[0].TrimStart('.') : "";
            string selected = EditorUtility.OpenFilePanel("Select File", startPath, ext);
            onSelected?.Invoke(string.IsNullOrEmpty(selected) ? null : selected);
#else
        // In build, use StandaloneFileBrowser (opens system dialog)
        ExtensionFilter[] filters = { new ExtensionFilter("Files", extensions) };
        StandaloneFileBrowser.OpenFilePanelAsync("Select File", startPath, filters, false, (paths) =>
        {
            if (paths.Length > 0)
                onSelected?.Invoke(paths[0]);
            else
                onSelected?.Invoke(null);
        });
#endif
        }
    }
}
