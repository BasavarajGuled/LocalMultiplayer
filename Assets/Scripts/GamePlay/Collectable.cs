using System.IO;
using UnityEngine;

public class Collectable : ObstacleCollectable
{
    public delegate void UpdateScore();
    public static event UpdateScore updateScore;
    protected override void HandleCollision(UnityEngine.Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            this.gameObject.SetActive(false);
            updateScore?.Invoke();
            SetHighScore();
        }
    }

    private void SetHighScore()
    {
        string highScorePath = Path.Combine(Application.persistentDataPath, "HighScore.json");

        if (File.Exists(highScorePath))
        {
            string json = File.ReadAllText(highScorePath);
            HighScore data = JsonUtility.FromJson<HighScore>(json);
            if (data.highScore < GameManager.Instance.uIController.Score)
            {
                GameManager.Instance.WriteHighScore(GameManager.Instance.uIController.Score);
            }
        }
        else
        {
            GameManager.Instance.WriteHighScore(GameManager.Instance.uIController.Score);
        }
    }
}
