using UnityEngine;

public class TimeScaleButton : MonoBehaviour
{
    private bool isTimeScale2x = false; // Tracks whether the time is currently toggled to 2x

    void OnEnable()
    {
        // Subscribe to events that may require resetting the time scale
        EndOfGame.OnEndOfGameActivated += ResetTimeScale;
    }

    void OnDisable()
    {
        // Unsubscribe from events to prevent memory leaks
        EndOfGame.OnEndOfGameActivated -= ResetTimeScale;
    }

    // Method to toggle time scale
    public void ToggleTimeScale()
    {
        if (isTimeScale2x)
        {
            // Reset to normal time
            Time.timeScale = 1f;
            isTimeScale2x = false;
        }
        else
        {
            // Set to 2x speed
            Time.timeScale = 2f;
            isTimeScale2x = true;
        }
    }

    // Method to reset time scale (called by external triggers)
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
        isTimeScale2x = false;
    }

    // Optional: Reset time scale when the scene reloads (just in case)
    void OnApplicationQuit()
    {
        Time.timeScale = 1f; // Ensures no lingering time changes when exiting play mode
    }
}