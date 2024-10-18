using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    public GameObject leaderboardEntryPrefab;
    public Transform content;

    private List<EndgameController.ScoreEntry> leaderboard = new List<EndgameController.ScoreEntry>();

    void Start()
    {
        LoadLeaderboard();
        DisplayLeaderboard();
    }

    // Hiển thị leaderboard trên màn hình
    private void DisplayLeaderboard()
    {
        for (int i = 0; i < leaderboard.Count; i++)
        {
            var entry = leaderboard[i];
            GameObject leaderboardEntry = Instantiate(leaderboardEntryPrefab, content);
            Text entryText = leaderboardEntry.GetComponent<Text>();
            Debug.Log($"Entry {i}: {entry.Name} - {entry.Score}");
            entryText.text = $"{i + 1}. {entry.Name} - {entry.Score}";
        }
    }

    // Tải leaderboard từ PlayerPrefs
    private void LoadLeaderboard()
    {
        leaderboard.Clear();
        int count = PlayerPrefs.GetInt("Leaderboard_Count", 0);
        Debug.Log("Leaderboard Count: " + count);

        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString($"Leaderboard_Name_{i}", "Unknown");
            float score = PlayerPrefs.GetFloat($"Leaderboard_Score_{i}", 0f);
            leaderboard.Add(new EndgameController.ScoreEntry { Name = name, Score = score });
        }
    }
}