using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleUI : MonoBehaviour
{
    public GameObject ui;

    public TMP_Text removeCost;
    public Button removeButton;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        removeCost.text = "$" + target.treeRemovalCost;
        
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Remove()
    {
        target.RemoveObstacle();
        BuildManager.instance.DeselectNode();
    }
}
