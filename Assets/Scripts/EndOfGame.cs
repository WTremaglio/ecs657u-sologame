using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Manages the End of Game UI and handles transition logic for the player.
/// </summary>
public class EndOfGame : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_Text _headerText;
    [SerializeField] private TMP_Text _roundsText;
    [SerializeField] private SceneFader _sceneTransition;

    [Header("Buttons")]
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _continueButton;

    /// <summary>
    /// Event triggered when the End of Game UI is activated.
    /// </summary>
    public static event System.Action OnEndOfGameActivated;

    private const string MainMenuSceneName = "MainMenu";

    /// <summary>
    /// Configures the End of Game screen based on the game outcome.
    /// </summary>
    /// <param name="isTrackComplete">Whether the track was completed successfully.</param>
    /// <param name="nextTrack">The name of the next track to unlock.</param>
    /// <param name="trackToUnlock">The index of the track to unlock.</param>
    public void Setup(bool isTrackComplete, string nextTrack, int trackToUnlock)
    {
        OnEndOfGameActivated?.Invoke();

        if (isTrackComplete)
        {
            ConfigureTrackCompleteUI(nextTrack, trackToUnlock);
        }
        else
        {
            ConfigureGameOverUI();
        }
    }

    /// <summary>
    /// Transitions the player to the main menu.
    /// </summary>
    public void Menu()
    {
        if (_sceneTransition == null)
        {
            Debug.LogError("SceneTransition is not assigned!");
            return;
        }
        _sceneTransition.FadeTo(MainMenuSceneName);
    }

    /// <summary>
    /// Reloads the current scene to retry the level.
    /// </summary>
    public void Retry()
    {
        if (_sceneTransition == null)
        {
            Debug.LogError("SceneTransition is not assigned!");
            return;
        }
        _sceneTransition.FadeTo(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Configures the UI for a completed track.
    /// </summary>
    private void ConfigureTrackCompleteUI(string nextTrack, int trackToUnlock)
    {
        _headerText.text = "Track Complete!";
        _retryButton.gameObject.SetActive(false);
        _continueButton.gameObject.SetActive(true);

        _continueButton.onClick.RemoveAllListeners();
        _continueButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("trackReached", trackToUnlock);
            if (_sceneTransition != null)
            {
                _sceneTransition.FadeTo(nextTrack);
            }
            else
            {
                Debug.LogError("SceneTransition is not assigned!");
            }
        });
    }

    /// <summary>
    /// Configures the UI for game over.
    /// </summary>
    private void ConfigureGameOverUI()
    {
        _headerText.text = "Game Over";
        _continueButton.gameObject.SetActive(false);
        _retryButton.gameObject.SetActive(true);
    }
}