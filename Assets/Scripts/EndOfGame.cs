using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndOfGame : MonoBehaviour
{
    public TMP_Text headerText;
    public TMP_Text roundsText;
    public SceneFader sceneTransition;

    [Header("Buttons")]
    public Button menuButton;
    public Button retryButton;
    public Button continueButton;

    public void Setup(bool isTrackComplete, string nextTrack, int trackToUnlock)
    {
        if (isTrackComplete)
        {
            headerText.text = "Track Complete!";
            retryButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(true);

            // Configure Continue Button
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() =>
            {
                PlayerPrefs.SetInt("trackReached", trackToUnlock);
                sceneTransition.FadeTo(nextTrack);
            });
        }
        else
        {
            headerText.text = "Game Over";
            continueButton.gameObject.SetActive(false);
        }
    }

    public void Menu()
    {
        sceneTransition.FadeTo("MainMenu");
    }

    public void Retry()
    {
        sceneTransition.FadeTo(SceneManager.GetActiveScene().name);
    }
}
