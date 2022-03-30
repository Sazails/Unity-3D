using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float health;
	public GameObject enemyObject;

	public void TakeDamage (float amount)
	{
		health -= amount;
		Debug.Log (health);
		if (health <= 0f) {
			Die ();
		}
	}

	void Die()
	{
		Destroy (enemyObject);
	}
}
