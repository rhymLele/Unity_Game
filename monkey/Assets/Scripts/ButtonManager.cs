using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
            {
                returnMenu();
            }
        }
    }
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
            }
            else
            {
                Time.timeScale = 1;
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
