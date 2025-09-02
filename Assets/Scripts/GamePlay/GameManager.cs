using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIController uIController;
    public static string highScorePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }


    }


    void Start()
    {
        highScorePath = Path.Combine(Application.persistentDataPath, "HighScore.json");

        if (File.Exists(highScorePath))
        {
            string json = File.ReadAllText(highScorePath);
            HighScore data = JsonUtility.FromJson<HighScore>(json);
            ShowHighScore(data.highScore);
        }
        else
            ShowHighScore(0);
    }

    public void ShowHighScore(int score)
    {
        uIController.ShowHighScore(score);
    }

    public void WriteHighScore(int score)
    {
        HighScore hScore = new HighScore()
        {
            highScore = score
        };
        string json = JsonUtility.ToJson(hScore, true);
        File.WriteAllText(highScorePath, json);
    }
}

[Serializable]
public class HighScore
{
    public int highScore;
}
