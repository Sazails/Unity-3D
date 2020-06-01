using UnityEngine;
using System.Collections;
using TMPro;

public class RandomEvents : MonoBehaviour
{
    public TextMeshProUGUI randomEventTimerText;
    public TextMeshProUGUI currentRandomEventText;

    public GameObject nextPieceHider;

    float timer = 60f;

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
            timer = 60f;
        }
    }

    public void SpawnRandomEvent()
    {
        int id = Random.Range(0, 5);
        if (id == 0)
        {
            nextPieceHider.SetActive(false);
            GridBase.scoreMultiplier = 1;

            SetCurrentRandomEvent("Speed change!");
            GridBase.fallTime = Random.Range(0.4f, 1f);

            Debug.Log("Fall speed changed to: " + GridBase.fallTime);
        }
        else if (id == 1)
        {
            nextPieceHider.SetActive(true);
            GridBase.scoreMultiplier = 1;

            SetCurrentRandomEvent("Piece hidden!");
        }
        else if (id == 2)
        {
            nextPieceHider.SetActive(false);
            GridBase.scoreMultiplier = 1;

            SetCurrentRandomEvent("Lines out!");

            for (int i = 2; i >= 0; i--)
            {
                GridBase.DeleteLine(i);
                GridBase.RowDown(i);
            }
        }
        else if (id == 3)
        {
            nextPieceHider.SetActive(false);

            SetCurrentRandomEvent("Double score!");

            GridBase.scoreMultiplier = 2;
        }
        else
        {
            SetCurrentRandomEvent("None");
        }
    }

    void SetCurrentRandomEvent(string txt)
    {
        currentRandomEventText.SetText("Current Event\n" + txt);
    }
}
