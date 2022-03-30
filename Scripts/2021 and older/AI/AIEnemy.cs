using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour {

	public Transform target;

	[Header("Projectile Settings")]
	public GameObject projectile;
	public int force;
	GameObject tempProjectile;


	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.LookAt (target.transform);
		GetComponent<NavMeshAgent> ().destination = target.transform.position;
		Aim ();
	}

	void Aim()
	{
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		RaycastHit hit;

		if (Physics.Raycast (transform.position, fwd, out hit)) 
		{
			if (hit.collider.gameObject.tag == "User") {
				Shoot ();
			}
		}
	}

	void Shoot()
	{
		tempProjectile = Instantiate (projectile, transform.position, transform.rotation) as GameObject;
		tempProjectile.GetComponent<Rigidbody> ().AddForce (tempProjectile.transform.forward * force);
		Destroy (tempProjectile, 4f);
	}
}
