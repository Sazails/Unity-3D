using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    public GameObject[] onToggle;
    public GameObject[] offToggle;

    public bool toggled = false;

    void Start()
    {
        toggled = !toggled;
        Toggle();
    }

    public void Toggle()
    {
        toggled = !toggled;
        if (toggled)
        {
            foreach (GameObject g in onToggle)
            {
                g.SetActive(true);
                Debug.Log(g.name + " activated.");
            }
            foreach (GameObject g in offToggle)
            {
                g.SetActive(false);
                Debug.Log(g.name + " deactivated.");
            }
        }
        else
        {
            foreach (GameObject g in onToggle)
            {
                g.SetActive(false);
                Debug.Log(g.name + " activated.");
            }
            foreach (GameObject g in offToggle)
            {
                g.SetActive(true);
                Debug.Log(g.name + " deactivated.");
            }
        }
    }
}
