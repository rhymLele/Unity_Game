using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresController : MonoBehaviour
{
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
