using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState{Spawning, Waiting, Counting};

	[System.Serializable] // Allows us to change values of instances in Unity
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public float rate;
	}

	public Wave[] waves;
	private int nextWave = 0;

	public Transform[] spawnPoints;

	public float timeBetweenWaves = 5f;
	public float waveCountdown;

	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.Counting;

	public Text currentWaveText;

	public Text timeTillNextWaveText;

	void Start()
	{
		if (spawnPoints.Length == 0) 
		{
			Debug.LogError ("No spawnPoints selected, choose at least 1");
		}

		waveCountdown = timeBetweenWaves;
	}

	void Update()
	{
		if (waveCountdown >= 2) {
			timeTillNextWaveText.text = waveCountdown.ToString ();
		} 
		else 
		{
			timeTillNextWaveText.text = "Started";
		}

		if (state == SpawnState.Waiting) 
		{
			if (!EnemyIsAlive ()) 
			{
				WaveCompleted ();
			} 
			else 
			{
				return;
			}
				
		}

		if (waveCountdown <= 0) {
			if (state != SpawnState.Spawning) 
			{
				StartCoroutine (SpawnWave (waves [nextWave]));
			}	
		} 
		else 
		{
			waveCountdown -= Time.deltaTime;
		}
	}


	void WaveCompleted()
	{
		Debug.Log ("Wave Completed!");

		currentWaveText.text = "Wave Completed!";

		state = SpawnState.Counting;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1) { 
			nextWave = 0;
			Debug.Log ("All waves are completed! Looping...");
		} 
		else 
		{
			nextWave++;
		}
	}


	bool EnemyIsAlive ()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f) 
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag ("Enemy") == null) 
			{
				return false;
			}
		}

		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		Debug.Log ("Spawning Wave: " + _wave.name);

		currentWaveText.text = _wave.name.ToString();

		state = SpawnState.Spawning;

		for (int i = 0; i < _wave.count; i++) 
		{
			SpawnEnemy (_wave.enemy);
			yield return new WaitForSeconds (1f / _wave.rate);
		}

		state = SpawnState.Waiting;
		yield break;
	}

	void SpawnEnemy (Transform _enemy)
	{
		Debug.Log("Spawning Enemy: " + _enemy.name);
		Transform _sp = spawnPoints[ Random.Range (0, spawnPoints.Length)];
		Instantiate (_enemy,_sp.position, _sp.rotation);
	}
}
