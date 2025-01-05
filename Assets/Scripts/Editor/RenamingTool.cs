using UnityEditor;
using UnityEngine;

public class RenamingTool : EditorWindow
{
    private string baseName = "GameObject";

    [MenuItem("Tools/Renaming Tool")]
    public static void ShowWindow()
    {
        GetWindow<RenamingTool>("Renaming Tool");
    }

    void OnGUI()
    {
        GUILayout.Label("Batch Renaming Tool", EditorStyles.boldLabel);

        // Field to set the base name
        baseName = EditorGUILayout.TextField("Base Name", baseName);

        if (GUILayout.Button("Rename Selected Objects"))
        {
            RenameSelectedObjects();
        }
    }

    private void RenameSelectedObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("No GameObjects selected for renaming.");
            return;
        }

        // Sort selected objects by hierarchy order
        System.Array.Sort(selectedObjects, (a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));

        // Rename each selected object
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            selectedObjects[i].name = $"{baseName} ({i + 1})";
        }

        Debug.Log($"Renamed {selectedObjects.Length} GameObjects to '{baseName} (X)'.");
    }
}