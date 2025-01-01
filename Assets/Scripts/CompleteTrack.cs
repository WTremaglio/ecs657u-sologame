using UnityEngine;

public class CompleteTrack : MonoBehaviour
{
    public string menuSceneName = "MainMenu";

    public string nextTrack = "Track02";
    public int trackToUnlock = 2;

    public SceneFader sceneFader;

    public void Continue()
    {
        Debug.Log("TRACK COMPLETED!");
        PlayerPrefs.SetInt("trackReached", trackToUnlock);
        sceneFader.FadeTo(nextTrack);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
