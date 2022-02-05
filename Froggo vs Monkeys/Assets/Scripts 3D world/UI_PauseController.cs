using UnityEngine;

public class UI_PauseController : MonoBehaviour
{
    public string escapeKey = "Pause";
    public GameObject pauseLayer;

    bool isPaused = false;
    bool hableToPause = true;

    private void Update()
    {
        if (Input.GetButtonDown(escapeKey) && hableToPause)
        {
            if (!isPaused)
                StartPause();
            else
                ClosePause();
        }
    }

    public void StartPause()
    {
        Time.timeScale = 0f;

        pauseLayer.SetActive(true);

        isPaused = true;
    }

    public void ClosePause()
    {
        Time.timeScale = 1f;

        pauseLayer.SetActive(false);

        isPaused = false;
    }
}
