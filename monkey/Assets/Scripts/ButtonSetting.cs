using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSetting : MonoBehaviour
{
    public GameObject menuPanal;  
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

}
