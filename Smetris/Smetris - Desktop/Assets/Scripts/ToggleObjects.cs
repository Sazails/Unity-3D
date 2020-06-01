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
                g.SetActive(true);
            foreach (GameObject g in offToggle)
                g.SetActive(false);
        }
        else
        {
            foreach (GameObject g in onToggle)
                g.SetActive(false);
            foreach (GameObject g in offToggle)
                g.SetActive(true);
        }
    }
}
