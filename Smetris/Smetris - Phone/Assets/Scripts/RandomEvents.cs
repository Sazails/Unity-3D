using UnityEngine;
using System.Collections;
using TMPro;

public class RandomEvents : MonoBehaviour
{
    public TextMeshProUGUI randomEventTimerText;
    public TextMeshProUGUI currentRandomEventText;

    public GameObject nextPieceHider;

    float timer = 20f;

    private void Start()
    {
        if (!PlayerPrefsX.GetBool("RandomEvents"))
        {
            Destroy(randomEventTimerText);
            Destroy(currentRandomEventText);
            Destroy(nextPieceHider);
            Destroy(this);
        }

        nextPieceHider.SetActive(false);
        SetCurrentRandomEvent("None");
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        randomEventTimerText.SetText("Random-Event-In\n" + (int)timer);
        if (timer < 0.1f)
        {
            SpawnRandomEvent();
            timer = 20f;
        }
    }

    public void SpawnRandomEvent()
    {
        int id = Random.Range(0, 4);
        if(id == 0)
        {
            GridBase.fallTime = Random.Range(0.4f, 1f);

            SetCurrentRandomEvent("Speed change");
            Debug.Log("Fall speed changed to: " + GridBase.fallTime);
        }
        else if(id == 1)
        {
            nextPieceHider.SetActive(true);
            SetCurrentRandomEvent("What's next?");
        }
        else if(id == 2)
        {
            nextPieceHider.SetActive(false);
            SetCurrentRandomEvent("That's next.");
        }
        else
        {
            SetCurrentRandomEvent("None");
        }
    }

    void SetCurrentRandomEvent(string txt)
    {
        currentRandomEventText.SetText("Current Event\n"+txt);
    }
}
