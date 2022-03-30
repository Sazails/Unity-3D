using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoCount : MonoBehaviour {

	public Text currentAmmo;
	public Text ammoInTotal;

	public Gun gun;

	void Start()
	{
		gun = GetComponent<Gun> ();
	}

	void Update()
	{
		currentAmmo.text = gun.currentAmmo.ToString ();
		ammoInTotal.text = gun.totalAmmo.ToString ();
	}
}
