using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button reload;
    public CanvasGroup gameOverPanel;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI highScoreText;

    private int score = 0;
    public int Score { get { return score; } }

    private StringBuilder scoreBuilder;

    void Start()
    {
        scoreBuilder = new StringBuilder("Score: 0");
        scoreText.text = scoreBuilder.ToString();

        reload.onClick.AddListener(() =>
        {
            OnReloadClick();
        });
    }

    void OnEnable()
    {
        Obstacle.reload += ShowGameOver;
        Collectable.updateScore += UpdateScore;
    }

    void OnDisable()
    {
        Obstacle.reload -= ShowGameOver;
        Collectable.updateScore += UpdateScore;
    }

    private void UpdateScore()
    {
        score += 10;

        scoreBuilder.Clear();
        scoreBuilder.Append("Score: ");
        scoreBuilder.Append(score);
        scoreText.text = scoreBuilder.ToString();
    }

    private void OnReloadClick()
    {
        Time.timeScale = 1f;
        Debug.Log("timeScale:" + Time.timeScale);
        SceneManager.LoadScene(1);
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("timeScale:" + Time.timeScale);
        gameOverPanel.alpha = 1f;
        gameOverPanel.blocksRaycasts = true;
        gameOverPanel.interactable = true;
    }

    internal void ShowHighScore(int highScore)
    {
        scoreBuilder = new StringBuilder("High Score: ");
        scoreBuilder.Append(highScore);
        highScoreText.text = scoreBuilder.ToString();
    }
}
