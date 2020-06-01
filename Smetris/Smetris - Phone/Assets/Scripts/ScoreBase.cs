using UnityEngine;
using TMPro;

public class ScoreBase : MonoBehaviour
{
    public int score;
    public int highscore;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        highscore = GetHighScore();
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        if(score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        currentScoreText.SetText("Current-Score\n" + score);
        highScoreText.SetText("High-Score\n" + highscore);
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("Highscore", 0);
    }
}
