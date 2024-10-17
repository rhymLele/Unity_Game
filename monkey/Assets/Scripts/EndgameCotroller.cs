using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameCotroller : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public InputField nameInput;
    public Button saveButton;
    public AudioController audioController;

    private float currentScore;
    private float highScore;
    private string highScoreName;

    void Start()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();

        // Lấy điểm từ PlayerPrefs và ép kiểu thành int
        currentScore = Mathf.RoundToInt(PlayerPrefs.GetFloat("CurrentScore", 0f));
        highScore = Mathf.RoundToInt(PlayerPrefs.GetFloat("HighScore", 0f));
        highScoreName = PlayerPrefs.GetString("HighScoreName", "None");

        // Hiển thị điểm số dưới dạng số nguyên
        scoreText.text = "Your Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore + " by " + highScoreName;

        // Kiểm tra nếu có âm thanh gameoverClip và phát
        if (audioController != null && audioController.gameoverClip != null)
        {
            audioController.PlaySFX(audioController.gameoverClip);
        }
        else
        {
            Debug.LogError("GameOverClip chưa được gán trong AudioController!");
        }

        // Nếu điểm hiện tại lớn hơn high score, hiển thị InputField để nhập tên
        if (currentScore > highScore)
        {
            nameInput.gameObject.SetActive(true);
            saveButton.gameObject.SetActive(true);
            saveButton.onClick.AddListener(SaveHighScore);
        }
        else
        {
            nameInput.gameObject.SetActive(false);
            saveButton.gameObject.SetActive(false);
        }
    }


    // Lưu high score mới cùng với tên người chơi
    private void SaveHighScore()
    {
        highScore = currentScore;
        highScoreName = nameInput.text;

        PlayerPrefs.SetFloat("HighScore", highScore);
        PlayerPrefs.SetString("HighScoreName", highScoreName);
        PlayerPrefs.Save();

        highScoreText.text = "High Score: " + highScore + " by " + highScoreName;

        // Ẩn input sau khi lưu
        nameInput.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
        // Bỏ điều kiện để kiểm tra
        /*nameInput.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);
        saveButton.onClick.AddListener(SaveHighScore);*/

    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
