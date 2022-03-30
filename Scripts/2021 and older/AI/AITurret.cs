using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurret : MonoBehaviour {

	//Turret stuff
	public Transform target;
	public float distance;
	public float alarmRange;
	public float firingRange;
	public AudioSource targetFound;
	public AudioSource targetLost;
	private bool canFire = false;
	//private TurretFiring turretFiring;


	//Bullet stuff
	public GameObject projectile;
	public int force;
	GameObject tempProjectile;


	void Update ()
	{
		transform.LookAt (target);
		distance = Vector3.Distance (transform.position, target.position);
		if(distance <= firingRange)
		{
			canFire = true;
			targetFound.Play();
		}
		else if(distance <= alarmRange)
		{
			canFire = false;
			targetLost.Play();
		}
	}

	void TurretFiring()
	{
		if (canFire == true) 
		{
			Shoot ();
			Debug.Log("Target is in sight");
		}
		if (canFire == false) 
		{
			Debug.Log ("Lost sight of target");
		}
	}

	void Shoot()
	{
		tempProjectile = Instantiate (projectile, transform.position, transform.rotation) as GameObject;
		tempProjectile.GetComponent<Rigidbody> ().AddForce (tempProjectile.transform.forward * force);
	}
}
