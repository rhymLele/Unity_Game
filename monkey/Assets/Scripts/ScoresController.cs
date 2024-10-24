using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresController : MonoBehaviour
{
    public void LoadLeaderboardScene()
    {
        SceneManager.LoadScene("LoadLeaderboard");
    }
}
