using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
        AudioListener.pause = false;
        Debug.Log("Scene " + name + " loaded.");
    }

    public static void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
        AudioListener.pause = false;
        Time.timeScale = 1;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        AudioListener.pause = false;
        Debug.Log("Scene restarted.");
    }
}
