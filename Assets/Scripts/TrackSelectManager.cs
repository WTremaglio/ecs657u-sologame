using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void UnlockAllTracks()
    {
        PlayerPrefs.SetInt("trackUnlocked", 3); // Assuming 3 is the highest track
        PlayerPrefs.Save();
        Debug.Log("All tracks unlocked!");
        
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}