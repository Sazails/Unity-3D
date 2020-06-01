using UnityEngine;
using TMPro;

public class SettingsBase : MonoBehaviour
{
    public TextMeshProUGUI randomEventsButtonText;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void ResetHighscore()
    {
        PlayerPrefs.DeleteKey("Highscore");
        Debug.Log("Highscore reset.");
    }

    public void ToggleRandomEvents()
    {
        PlayerPrefsX.SetBool("RandomEvents", PlayerPrefsX.GetBool("RandomEvents", false) ? false : true);
        UpdateUI();
        Debug.Log("Random events toggled.");
    }

    void UpdateUI()
    {
        randomEventsButtonText.SetText(PlayerPrefsX.GetBool("RandomEvents", false) ? "Disable Random Events" : "Enable Random Events");
    }
}
