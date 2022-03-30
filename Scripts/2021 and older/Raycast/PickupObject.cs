using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupObject : MonoBehaviour {

	public PlayersFuel fuel;
	public int fuelToAdd;

	public Text objectInfoText;

	void Start()
	{
		objectInfoText.text = "";
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 fwd = Camera.main.transform.TransformDirection (Vector3.forward);
		RaycastHit hit;

		if (Physics.Raycast (transform.position, fwd, out hit)) 
		{
			if (hit.distance <= 4.0 && hit.collider.gameObject.tag == "Fuel") {
				objectInfoText.text = hit.collider.gameObject.name.ToString ();

				if (Input.GetKeyDown (KeyCode.E) && hit.collider.gameObject.name == "Fuel") {
					fuel.currentFuel += fuelToAdd;
					Destroy (hit.collider.gameObject);
				}
			} else {
				objectInfoText.text = "";
			}
		}
	}
}
