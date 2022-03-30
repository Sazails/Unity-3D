using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour {

	[Header ("Point System")]
	public Text pointsText;
	public int currentPoints;
	public int startPoints;
	public int pointsLeftToEscape;
	public int neededPointsToEscape;

	[Header (" Results Canvas ")]
	public Button backButton;
	public Text statusText;
	public Text totalPointsText;
	public Text pointsNeededToEscape;

	// Use this for initialization
	void Start () 
	{
		currentPoints = startPoints;

		pointsText.text = "Points: " + currentPoints;

		pointsLeftToEscape = neededPointsToEscape - currentPoints;

		ResultsCanvas ();

		/*if(currentPoints == pointsToEscape)
		{
			
		}


		/* Keep money on respawn!!!
		if (PlayerPrefs.HasKey ("CurrentMoney")) 
		{
			currentMoney = PlayerPrefs.GetInt ("CurrentMoney");
		} 
		else 
		{
			currentMoney = 0;
			PlayerPrefs.SetInt ("CurrentMoney", 0);

		}
		*/
	}

	public void AddMoney(int moneyToAdd)
	{
		currentPoints += moneyToAdd;
		PlayerPrefs.SetInt ("CurrentPoints", currentPoints);
		pointsText.text = "Points: " + currentPoints;
	}

	void ResultsCanvas()
	{
		//if(Gameobject is active this script runs)
		totalPointsText.text = "Points In Total: " + currentPoints.ToString();
		pointsNeededToEscape.text = "Needed To Escape: " + pointsLeftToEscape.ToString ();
		statusText.text = "Status: " + "Dead".ToString ();
	}
}
