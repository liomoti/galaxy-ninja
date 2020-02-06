using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinsSpawner : MonoBehaviour
{
	public Transform[] spawnPoints;
	public GameObject coinsPrefab;
	public GameObject shieldPrefab;
	public GameObject heartPrefab;

	//coins
	private float timeToSpawn = 9f;
	private int timeBetweenWaves = 25;
	//shield
	private float timeToSpawnShield = 20f;
	private int timeBetweenWavesShield = 40;
	//heart
	private float timeToSpawnHeart;
	private int timeBetweenWavesHeart = 100;

	void Awake()
	{
		timeToSpawnHeart = UnityEngine.Random.Range(100, 250);
		Debug.Log("timeToSpawnHeart: " + timeToSpawnHeart);

	}
	// Update is called once per frame
	void Update()
	{
		//System.Random rand = new System.Random();

		
		if (Time.time >= timeToSpawn)
		{
			spawnCoins();
			timeToSpawn = Time.time + timeBetweenWaves;
			//Debug.Log("Time: " + Time.time.ToString("0") + " Next time To Spawn: " + timeToSpawn.ToString("0"));
		}
		else if (Time.time >= timeToSpawnShield)
		{
			spawnShield();
			timeToSpawnShield = Time.time + timeBetweenWavesShield;			
		}
		else if ((Time.time >= (DragFingerMove.collisionTime + timeToSpawnHeart)) && GameManager.lifes_count < 1)
		{
			spawnHeart();
			timeToSpawnHeart = Time.time + timeBetweenWavesHeart;			
		}

	}

	//spawn coins
	void spawnCoins()
	{
		int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);

		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (randomIndex == i)
			{
				//spawn that asteroid!
				Instantiate(coinsPrefab, spawnPoints[i].position, Quaternion.identity);
			}
		}
	}

	//spawn sheild
	void spawnShield()
	{
		int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);

		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (randomIndex == i)
			{
				//spawn that sheild!
				Instantiate(shieldPrefab, spawnPoints[i].position, Quaternion.identity);
			}
		}
	}

	//spawn heart
	void spawnHeart()
	{
		int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);

		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (randomIndex == i)
			{
				//spawn that heart!
				Instantiate(heartPrefab, spawnPoints[i].position, Quaternion.identity);
			}
		}
	}
}
