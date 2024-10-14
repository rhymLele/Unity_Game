using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("NightScene");
    }

    public void returnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
