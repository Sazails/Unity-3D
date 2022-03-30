using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleApplication : MonoBehaviour {

	[Header ("Codes")]
	private string code1;
	private string code2;
	private string code3;

	private float quessedCode;

	[SerializeField]
	private Text errorText;

	[SerializeField]
	private InputField input;

	void Awake()
	{
		//	code1 = theCode1(); 
		// errorText will be used for if code is not correct
	}

	public void GetInput(string code)
	{
		Debug.Log ("You Entered: " + code);
		input.text = "";

		if (code != "Test") 
		{
			input.text = "Invalid";
		} 
		else if (code == "Test") 
		{
			input.text = "Yes it is..";
			Debug.Log ("Code granted");
		}
	}
}
