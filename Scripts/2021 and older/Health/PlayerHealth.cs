using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;
	public Text healthText;
	public Slider healthBarSlider;

	public GameObject uiCanvas;
	public GameObject deadCanvas;

	void Start()
	{
		currentHealth = maxHealth;
		healthBarSlider.maxValue = maxHealth;
		InvokeRepeating("HealthCheck", 0, 1f);
	}

	void HealthCheck()
	{
		healthBarSlider.value = currentHealth;
		healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
		if (currentHealth < 1)
		{
			Die();
		}
		if (currentHealth > 100)
		{
			currentHealth = maxHealth;
		}
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
	}

	void Die()
	{
		uiCanvas.SetActive(false);
		deadCanvas.SetActive(true);
		Destroy (gameObject);
	}
}




