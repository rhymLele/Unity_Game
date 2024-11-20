using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonSetting : MonoBehaviour
{
    public GameObject menuPanal;  // Reference to the menu panel

    public Text gamemode;  
    public static string selectedMode = "NightScene";
    public static string getMode = "Normal";

    private void Start()
    {
        gamemode.text = "Gamemode: " + getMode;
    }
    // This method will be called when the Setting button is clicked
    public void OnSettingButtonClick()
    {
        AudioController audioController = FindObjectOfType<AudioController>();
        if (audioController != null)
        {
            audioController.PlayButtonClick();
        }
        if (menuPanal != null)
        {
            bool isActive = menuPanal.activeSelf;
            menuPanal.SetActive(!isActive);
        }
    }

    public void startGame()
    {
        AudioController audioController = FindObjectOfType<AudioController>();

        if (audioController != null)
        {

            audioController.PlayButtonClickAndChangeScene(selectedMode);
        }
        else
        {
            SceneManager.LoadScene(selectedMode);
        }
        Time.timeScale = 1;
    }

    public void setNormalMode()
    {
        selectedMode = "NightScene";
        getMode = "Normal";
        gamemode.text = "Gamemode: " + getMode;
    }

    public void setMode1()
    {
        selectedMode = "Mode1Scene";
        getMode = "Mode1";
        gamemode.text = "Gamemode: " + getMode;
    }
    public void setMode2()
    {
        selectedMode = "Mode2Scene";
        getMode = "Mode2";
        gamemode.text = "Gamemode: " + getMode;
    }
}
