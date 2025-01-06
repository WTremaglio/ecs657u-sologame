using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the functionality of the "Next Round" button, including interactivity and triggering the next round.
/// </summary>
public class NextRoundButton : MonoBehaviour
{
    [Header("Button Configuration")]
    [SerializeField] private Button _button;

    private bool _lastInteractableState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_button == null)
        {
            Debug.LogError("NextRoundButton: Button is not assigned. Please assign it in the Inspector.");
            return;
        }

        _button.onClick.AddListener(HandleNextRoundButtonClick);
        _lastInteractableState = _button.interactable; // Initialize tracking variable
    }

    // Update is called once per frame
    void Update()
    {
        if (_button == null) return;

        // Determine if the button should be interactable
        bool canStartNextRound = RoundManager.Instance != null &&
                                 !RoundManager.Instance.RoundInProgress &&
                                 RoundManager.EnemiesAlive == 0;

        // Only update the button's interactable state if it has changed
        if (_lastInteractableState != canStartNextRound)
        {
            _button.interactable = canStartNextRound;
            _lastInteractableState = canStartNextRound;
        }
    }

    /// <summary>
    /// Handles the button click event and starts the next round.
    /// </summary>
    private void HandleNextRoundButtonClick()
    {
        if (RoundManager.Instance == null)
        {
            Debug.LogError("NextRoundButton: RoundManager instance is not available.");
            return;
        }

        RoundManager.Instance.StartNextRound();
    }
}