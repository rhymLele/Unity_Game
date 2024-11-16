using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour
{
    public void LoadMenuScene()
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

    }
}
