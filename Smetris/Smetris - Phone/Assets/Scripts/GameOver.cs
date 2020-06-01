using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public ScoreBase scoreBase;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    public void Trigger()
    {
        gameOverScreen.SetActive(true);
        scoreText.SetText("Score: " + scoreBase.score);
        highscoreText.SetText("Highscore: " + scoreBase.highscore);
    }
}
