using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameController : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public InputField nameInput;
    public Button saveButton;
    public AudioController audioController;

    private float currentScore;
    private List<ScoreEntry> leaderboard = new List<ScoreEntry>();
    private const int MaxEntries = 10;

    void Start()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();

        currentScore = Mathf.RoundToInt(PlayerPrefs.GetFloat("CurrentScore", 0f));

        LoadLeaderboard();

        scoreText.text = "Your Score: " + currentScore;
        DisplayHighScore();

        if (audioController != null && audioController.gameoverClip != null)
        {
            audioController.PlaySFX(audioController.gameoverClip);
        }
        else
        {
            Debug.LogError("GameOverClip chưa được gán trong AudioController!");
        }
        nameInput.text = GetLastPlayerNameOrDefault();
        nameInput.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);
        saveButton.onClick.RemoveAllListeners();
        saveButton.onClick.AddListener(SaveScore);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SaveScore();
            SceneManager.LoadScene(ButtonSetting.selectedScene);
        }
        if (Input.GetKeyDown(KeyCode.Space)
        ){
            SceneManager.LoadScene(ButtonSetting.selectedScene);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
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

    private string GetLastPlayerNameOrDefault()
    {
        string lastPlayerName = PlayerPrefs.GetString("LastPlayerName", "");

        if (!string.IsNullOrEmpty(lastPlayerName))
        {
            return lastPlayerName;
        }

        if (leaderboard.Count > 0)
        {
            return leaderboard[0].Name;
        }
        return "Player";
    }
    private void DisplayHighScore()
    {
        if (leaderboard.Count > 0)
        {
            var topEntry = leaderboard[0];
            highScoreText.text = "High Score: " + topEntry.Score + " by " + topEntry.Name;
        }
        else
        {
            highScoreText.text = "High Score: None";
        }
    }
    private void SaveLeaderboard()
    {
        for (int i = 0; i < leaderboard.Count; i++)
        {
            PlayerPrefs.SetString($"Leaderboard_Name_{i}", leaderboard[i].Name);
            PlayerPrefs.SetFloat($"Leaderboard_Score_{i}", leaderboard[i].Score);
        }
        PlayerPrefs.SetInt("Leaderboard_Count", leaderboard.Count);
        PlayerPrefs.Save();
    }
    private void LoadLeaderboard()
    {
        leaderboard.Clear();
        int count = PlayerPrefs.GetInt("Leaderboard_Count", 0);

        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString($"Leaderboard_Name_{i}", "Unknown");
            float score = PlayerPrefs.GetFloat($"Leaderboard_Score_{i}", 0f);
            leaderboard.Add(new ScoreEntry { Name = name, Score = score });
        }
    }
    private void SaveScore()
    {
        string playerName = string.IsNullOrWhiteSpace(nameInput.text) ?
                        GetLastPlayerNameOrDefault() :
                        nameInput.text;
        PlayerPrefs.SetString("LastPlayerName", playerName);
        leaderboard.Add(new ScoreEntry { Name = playerName, Score = currentScore });
        leaderboard.Sort((a, b) => b.Score.CompareTo(a.Score));

        if (leaderboard.Count > MaxEntries)
        {
            leaderboard.RemoveAt(leaderboard.Count - 1);
        }

        SaveLeaderboard();
        DisplayHighScore();
        AudioController audioController = FindObjectOfType<AudioController>();

        if (audioController != null)
        {
            audioController.PlayButtonClick();
        }
    }

    [System.Serializable]
    public class ScoreEntry
    {
        public string Name;
        public float Score;
    }
}
