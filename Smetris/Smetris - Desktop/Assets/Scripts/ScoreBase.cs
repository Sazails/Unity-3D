using UnityEngine;
using TMPro;

public class ScoreBase : MonoBehaviour
{
    public int score;
    public int highscore;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    bool eventsOn;

    void Start()
    {
        eventsOn = PlayerPrefsX.GetBool("RandomEvents", false);
        highscore = GetHighScore();
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score > highscore)
        {
            highscore = score;
            if (eventsOn)
            {
                PlayerPrefs.SetInt("EventsHighscore", highscore);
            }
            else
            {
                PlayerPrefs.SetInt("Highscore", highscore);
            }
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
        if (eventsOn)
        {
            return PlayerPrefs.GetInt("EventsHighscore", 0);
        }
        return PlayerPrefs.GetInt("Highscore", 0);
    }
}
