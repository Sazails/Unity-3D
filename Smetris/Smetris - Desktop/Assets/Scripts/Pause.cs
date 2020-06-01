using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject[] onPause;
    public GameObject[] offPause;

    public bool paused = false;

    private void Start()
    {
        paused = !paused;
        Toggle();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }

    public void Toggle()
    {
        paused = !paused;
        if (paused)
        {
            foreach (GameObject g in onPause)
                g.SetActive(true);
            foreach (GameObject g in offPause)
                g.SetActive(false);

            AudioListener.pause = true;
            Time.timeScale = 0;
        }
        else
        {
            foreach (GameObject g in onPause)
                g.SetActive(false);
            foreach (GameObject g in offPause)
                g.SetActive(true);

            AudioListener.pause = false;
            Time.timeScale = 1;
        }
    }
}
