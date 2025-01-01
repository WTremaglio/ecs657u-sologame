using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public EndOfGame endOfGameUI;

    [Header("Track Progression")]
    public string nextTrack;
    public int trackToUnlock;

    // Start is called before the first frame update
    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
        {
            return;
        }
        if (PlayerStats.Lives <= 0)
        {
            EndGame(false);
        }
    }

    public void WinLevel()
    {
        EndGame(true);
    }

    void EndGame(bool isTrackComplete)
    {
        GameIsOver = true;

        endOfGameUI.gameObject.SetActive(true);
        endOfGameUI.Setup(isTrackComplete, nextTrack, trackToUnlock);
    }
}