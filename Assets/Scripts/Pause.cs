using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public PlayerController script;
    public static float standardTimeScale = 1f;
    bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause1();
            }
            else if (!isPaused)
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);

        script.enabled = true;
        Time.timeScale = 1f;

        ResumeAllAudio();
        isPaused = false;
    }

    void Pause1()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);

        script.enabled = false;
        Time.timeScale = 0f;

        PauseAllAudio();
        isPaused = true;
    }

    private void PauseAllAudio()
    {
        // Find all AudioSources in the scene
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Pause all AudioSources
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].Pause();
        }
    }

    private void ResumeAllAudio()
    {
        // Find all AudioSources in the scene
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Resume all AudioSources
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].UnPause();
        }
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
