using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
	public Transform[] spawnPoints;
	public GameObject asteroidPrefab;
	public GameObject fireAsteroidPrefab;
	public GameObject bigAsteroidPrefab;
	public GameObject smallAsteroidPrefab;
	private float timeToSpawn = 2f;
	private float fastTimeToSpawn = 2f;
	private int timeBetweenWaves = 3;
	private int timeBetweenSecondWaves = 1;


	// Update is called once per frame
	void Update()
	{
		if (Score.score < 700)
		{
			if (Time.time >= timeToSpawn)
			{
				spawnAsteroids();
				timeToSpawn = Time.time + timeBetweenWaves;
				//Debug.Log("Time: " + Time.time.ToString("0") + " Next time To Spawn: " + timeToSpawn.ToString("0"));
			}
		}else if(Score.score < 1500)
		{
			if (Time.time >= timeToSpawn)
			{
				spawnFireAsteroids();
				timeToSpawn = Time.time + timeBetweenSecondWaves;
				//Debug.Log("Time: " + Time.time.ToString("0") + " Next time To Spawn: " + timeToSpawn.ToString("0"));
				//for next level:
				fastTimeToSpawn = Time.time + timeBetweenSecondWaves;
			}
		}
		else if (Score.score < 3000)
		{
			if (Time.time >= timeToSpawn)
			{
				spawnBigAsteroids();
				
				timeToSpawn = Time.time + timeBetweenWaves;
				//Debug.Log("Time: " + Time.time.ToString("0") + " Next time To Spawn: " + timeToSpawn.ToString("0"));
			}
			if (Time.time >= fastTimeToSpawn)
			{
				spawnSmallAsteroids();
				fastTimeToSpawn = Time.time + timeBetweenSecondWaves;
				//Debug.Log("Time: " + Time.time.ToString("0") + " Next time To Spawn: " + timeToSpawn.ToString("0"));
			}
		}




	}

	void spawnAsteroids()
	{
		int randomIndex = Random.Range(0, spawnPoints.Length);

		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (randomIndex == i)
			{
				//spawn that asteroid!
				Instantiate(asteroidPrefab, spawnPoints[i].position, Quaternion.identity);
			}
		}
	}
	void spawnFireAsteroids()
	{
		int randomIndex = Random.Range(0, spawnPoints.Length);

		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (randomIndex == i)
			{
				//spawn that asteroid!
				Instantiate(fireAsteroidPrefab, spawnPoints[i].position, Quaternion.identity);
			}
		}
	}

	void spawnBigAsteroids()
	{
		int randomIndex = Random.Range(0, spawnPoints.Length);

		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (randomIndex == i)
			{
				//spawn that asteroid!
				Instantiate(bigAsteroidPrefab, spawnPoints[i].position, Quaternion.identity);
			}
		}
	}
	void spawnSmallAsteroids()
	{
		int randomIndex = Random.Range(0, spawnPoints.Length);

		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (randomIndex == i)
			{
				//spawn that asteroid!
				Instantiate(smallAsteroidPrefab, spawnPoints[i].position, Quaternion.identity);
			}
		}
	}

}
