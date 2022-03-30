using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour {

	Animator anim;

	public float lowestDamage = 10f;
	public float highestDamage = 20f;
	public float range = 100f;
	public float fireRate = 15f;
	public float impactForce = 30f;

	public int totalAmmo = 50;
	public int maxAmmo = 10;
	public int currentAmmo;
	public float reloadTime = 2f;
	private bool isReloading = false;

	public int coinValue;
	public Points thePoints;

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;

	private float nextTimeToFire = 0f;

	public AudioSource fireSound;


	//public Animator animator;

	void Start()
	{
		currentAmmo = maxAmmo;
		thePoints = FindObjectOfType<Points> ();
		anim = GetComponent<Animator> ();
	}

	void OnEnable()
	{
		isReloading = false;
		//animator.SetBool ("Reloading", false);
	}

	// Update is called once per frame
	void Update () {

		if (isReloading)
			return;

		if (currentAmmo <= 0) {
			StartCoroutine (Reload ());
			return;
		}

		if (Input.GetKey (KeyCode.Mouse0) && Time.time >= nextTimeToFire) {
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot ();
		}

		if (Input.GetKeyDown (KeyCode.R)) 
		{
			if (totalAmmo >= 1 && currentAmmo != maxAmmo) {
				StartCoroutine (ReloadOnPress ());
				totalAmmo -= currentAmmo;
				currentAmmo = maxAmmo;
				return;
			}
		}

		if (totalAmmo <= 0) {
			totalAmmo = 0;
		}

	}
		
	IEnumerator ReloadOnPress()
	{
		isReloading = true;
		Debug.Log ("Reloading..");

		anim.Play ("Reload");

		//animator.SetBool ("Reloading", true);

		yield return new WaitForSeconds (reloadTime - .25f);

		//animator.SetBool ("Reloading", false);

		yield return new WaitForSeconds (.25f);

		isReloading = false;
	}

	IEnumerator Reload()
	{
		if (totalAmmo >= 1) 
		{
			isReloading = true;
			Debug.Log ("Reloading..");

			anim.Play ("Reload");

			//animator.SetBool ("Reloading", true);

			yield return new WaitForSeconds (reloadTime - .25f);

			//animator.SetBool ("Reloading", false);

			yield return new WaitForSeconds (.25f);

			currentAmmo = maxAmmo;
			totalAmmo -= maxAmmo;
			isReloading = false;
		}

	}

	void Shoot()
	{
		//fireSound.Play ();
		//muzzleFlash.Play ();

		currentAmmo--;

		anim.Play ("Shoot");


		RaycastHit hit;
		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) 
		{
			Debug.Log (hit.transform.name);

			EnemyHealth target = hit.transform.GetComponent<EnemyHealth> ();
			if(target != null)
			{
				thePoints.AddMoney (coinValue);
				target.TakeDamage (Random.Range(lowestDamage, highestDamage));
			}

			if(hit.rigidbody != null)
			{
				hit.rigidbody.AddForce (-hit.normal * impactForce);
			}

			//GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			//Destroy (impactGO, 2f);
		}
	}

}
