using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void returnMenu()
    {
        AudioController audioController = FindObjectOfType<AudioController>();

        if (audioController != null)
        {

            audioController.PlayButtonClickAndChangeScene("Menu");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }

        Time.timeScale = 1;
    }
    public void Pause()
    {
        AudioController audioController = FindObjectOfType<AudioController>();

        if (audioController != null)
        {
            audioController.PlayButtonClick();
        }
        if (pauseMenu != null)
        {
            bool isActive = pauseMenu.activeSelf;
            pauseMenu.SetActive(!isActive);
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0; 
                Debug.Log("Game paused, Time.timeScale set to 0");
            }
            else
            {
                Time.timeScale = 1; 
                Debug.Log("Game resumed, Time.timeScale set to 1");
            }

            Debug.Log("Pause Menu new state: " + pauseMenu.activeSelf);
        }
    }
    public void Resume()
    {
        AudioController audioController = FindObjectOfType<AudioController>();

        if (audioController != null)
        {
            audioController.PlayButtonClick();
        }
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        AudioController audioController = FindObjectOfType<AudioController>();

        if (audioController != null)
        {
            audioController.PlayButtonClick(); 
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
