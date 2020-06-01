using UnityEngine;
using TMPro;

public class SettingsBase : MonoBehaviour
{
    public TextMeshProUGUI randomEventsButtonText;
    public TextMeshProUGUI highscoresText;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void ResetHighscore()
    {
        PlayerPrefs.DeleteKey("Highscore");
        Debug.Log("Highscore reset.");
        UpdateUI();
    }

    public void ResetEventsHighscore()
    {
        PlayerPrefs.DeleteKey("EventsHighscore");
        Debug.Log("Events Highscore reset.");
        UpdateUI();
    }

    public void ToggleRandomEvents()
    {
        PlayerPrefsX.SetBool("RandomEvents", PlayerPrefsX.GetBool("RandomEvents", false) ? false : true);
        Debug.Log("Random events toggled.");
        UpdateUI();
    }

    void UpdateUI()
    {
        randomEventsButtonText.SetText(PlayerPrefsX.GetBool("RandomEvents", false) ? "Disable Random Events" : "Enable Random Events");
        highscoresText.SetText("Highscore: " + PlayerPrefs.GetInt("Highscore", 0) + "\n\nEvents Highscore: " + PlayerPrefs.GetInt("EventsHighscore", 0));
    }
}
