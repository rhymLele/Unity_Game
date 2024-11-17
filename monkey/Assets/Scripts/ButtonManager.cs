using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;

    public void startGame()
    {
        AudioController audioController = FindObjectOfType<AudioController>();

        if (audioController != null)
        {

            audioController.PlayButtonClickAndChangeScene("NightScene");
        }
        else
        {
            SceneManager.LoadScene("NightScene");
        }
        Time.timeScale = 1; 
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
            Time.timeScale = isActive ? 1 : 0;
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
