using UnityEngine;
using UnityEngine.UI;

public class TrackSelectManager : MonoBehaviour
{
    public SceneFader fader;

    public Button[] trackButtons;

    // Start is called before the first frame update
    void Start ()
    {
        int trackUnlocked = PlayerPrefs.GetInt("trackUnlocked", 1);

        for (int i = 0; i < trackButtons.Length; i++)
        {
            if (i + 1 > trackUnlocked)
                trackButtons[i].interactable = false;
        }
    }

    public void Select (string levelName)
    {
        fader.FadeTo(levelName);
    }
}