using UnityEditor;
using UnityEngine;

public class NodeEditorWindow : EditorWindow
{
    private GameObject selectedPrefab;
    private string baseName = "Node";

    [MenuItem("Tools/Node Editor")]
    public static void ShowWindow()
    {
        GetWindow<NodeEditorWindow>("Node Editor");
    }

    void OnGUI()
    {
        GUILayout.Label("Node Customization", EditorStyles.boldLabel);

        // Field to select the prefab
        selectedPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab", selectedPrefab, typeof(GameObject), false);

        // Field to set the base name
        baseName = EditorGUILayout.TextField("Base Name", baseName);

        if (GUILayout.Button("Apply Prefab and Rename"))
        {
            ApplyPrefabAndRename();
        }

        if (GUILayout.Button("Group Nodes into Rows"))
        {
            GroupNodesIntoRows();
        }
    }

    private void ApplyPrefabAndRename()
    {
        if (selectedPrefab == null)
        {
            Debug.LogError("No prefab selected!");
            return;
        }

        GameObject[] selectedObjects = Selection.gameObjects;

        // Sort by hierarchy order for consistent renaming
        System.Array.Sort(selectedObjects, (a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            GameObject selectedObject = selectedObjects[i];

            // Replace with the selected prefab
            GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(selectedPrefab, selectedObject.transform.parent);
            newObject.transform.position = selectedObject.transform.position;
            newObject.transform.rotation = selectedObject.transform.rotation;

            // Rename the new object
            newObject.name = $"{baseName} ({i + 1})";

            // Destroy the old object
            DestroyImmediate(selectedObject);
        }

        Debug.Log("Prefab applied and objects renamed!");
    }

    private void GroupNodesIntoRows()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        // Error checking: Ensure there are exactly 256 selected nodes
        if (selectedObjects.Length != 256)
        {
            Debug.LogError("You must select exactly 256 nodes to group them into rows.");
            return;
        }

        // Sort by hierarchy order for consistent grouping
        System.Array.Sort(selectedObjects, (a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));

        // Create empty parent GameObjects for each row and group nodes
        for (int row = 0; row < 16; row++)
        {
            // Create a new empty GameObject for the row
            GameObject rowParent = new GameObject($"Row {row + 1}");

            // Parent the next 16 nodes to this row
            for (int i = 0; i < 16; i++)
            {
                int index = row * 16 + i;
                GameObject node = selectedObjects[index];
                node.transform.SetParent(rowParent.transform);
            }
        }

        Debug.Log("Nodes grouped into 16 rows successfully.");
    }
}