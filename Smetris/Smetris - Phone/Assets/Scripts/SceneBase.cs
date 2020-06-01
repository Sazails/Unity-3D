using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
        Debug.Log("Scene " + name + " loaded.");
    }

    public static void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Debug.Log("Scene restarted.");
    }
}
