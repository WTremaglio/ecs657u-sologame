using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ObstacleEditor : MonoBehaviour
{
    [MenuItem("Tools/Assign Obstacles to Nodes")]
    static void AssignObstaclesToNodes()
    {
        // Create a dictionary to map positions to nodes
        Dictionary<Vector3, Node> nodeMap = new Dictionary<Vector3, Node>();
        Node[] nodes = Object.FindObjectsByType<Node>(FindObjectsSortMode.None);

        // Populate the dictionary with nodes based on their positions
        foreach (Node node in nodes)
        {
            Vector3 position = new Vector3(node.transform.position.x, 0, node.transform.position.z);
            if (!nodeMap.ContainsKey(position))
            {
                nodeMap[position] = node;
            }
        }

        GameObject[] obstacles = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        // Match obstacles to nodes using the dictionary
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.CompareTag("Tree") || obstacle.CompareTag("Rock"))
            {
                Vector3 position = new Vector3(obstacle.transform.position.x, 0, obstacle.transform.position.z);
                if (nodeMap.TryGetValue(position, out Node matchingNode))
                {
                    Undo.RecordObject(matchingNode, "Assign Obstacle");
                    matchingNode.obstacle = obstacle;
                    EditorUtility.SetDirty(matchingNode); // Mark the object as dirty
                }
            }
        }

        Debug.Log("Obstacles assigned to nodes. Don't forget to save the scene!");
    }
}