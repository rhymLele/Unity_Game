using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonSetting : MonoBehaviour
{
    public GameObject menuPanal;

    public Text gamemode;  
    public static string selectedScene = "NightScene";
    public static string getMode = "Normal";

    private void Start()
    {
        gamemode.text = "Gamemode: " + PlayerPrefs.GetString("mode").ToString();
    }
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
        selectedScene = PlayerPrefs.GetString("scene").ToString() ?? "NightScene";
        AudioController audioController = FindObjectOfType<AudioController>();
        if (audioController != null)
        {
            audioController.PlayButtonClickAndChangeScene(selectedScene);
        }
        else
        {
            SceneManager.LoadScene(selectedScene);
        }
        Time.timeScale = 1;
    }

    private void getModeData(string getmode, string selectedscene)
    {
        PlayerPrefs.SetString("mode", getmode);
        PlayerPrefs.SetString("scene", selectedscene);
        PlayerPrefs.Save();
        gamemode.text = "Gamemode: " + PlayerPrefs.GetString("mode").ToString();

    }
    public void setNormalMode()
    {
        getModeData("Normal", "NightScene");
    }
    public void setMode1()
    {
        getModeData("Mode1", "Mode1Scene");
    }
    public void setMode2()
    {
        getModeData("Mode2", "Mode2Scene");
    }

    public void LoadLeaderboardScene()
    {
        AudioController audioController = FindObjectOfType<AudioController>();

        if (audioController != null)
        {

            audioController.PlayButtonClickAndChangeScene("LoadLeaderboard");
        }
        else
        {
            SceneManager.LoadScene("LoadLeaderboard");
        }

    }
}
