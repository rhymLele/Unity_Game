using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
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
            // Cập nhật trạng thái Time.timeScale dựa trên trạng thái của pauseMenu
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0; // Tạm dừng game
                Debug.Log("Game paused, Time.timeScale set to 0");
            }
            else
            {
                Time.timeScale = 1; // Tiếp tục game
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
