using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSetting : MonoBehaviour
{
    public GameObject menuPanal;  // Reference to the menu panel

    // This method will be called when the Setting button is clicked
    public void OnSettingButtonClick()
    {
        AudioController audioController = FindObjectOfType<AudioController>();
        if (audioController != null)
        {
            audioController.PlayButtonClick();
        }
        // Toggle the menu panel visibility
        if (menuPanal != null)
        {
            bool isActive = menuPanal.activeSelf;
            menuPanal.SetActive(!isActive);  // Show if hidden, hide if shown
        }
    }

}
