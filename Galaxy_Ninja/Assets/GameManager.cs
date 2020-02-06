using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject MyCharacter1;
	public GameObject MyCharacter2;
	public GameObject MyCharacter3;
	public GameObject[] lifes;
	public float slowness = 10f;
	public GameObject GameUI;
	public Text EndGameScoreText;	
	public static int lifes_count = 2;
	public static int level = 1; //1,2,3

	public void Start()
	{
		Debug.Log("GameManager Start!");
		if (CharacterSelect.CurrentCharacter == 1)
		{
			MyCharacter1.SetActive(true);
			MyCharacter2.SetActive(false);
			MyCharacter3.SetActive(false);
		}
		else if (CharacterSelect.CurrentCharacter == 2)
		{
			MyCharacter1.SetActive(false);
			MyCharacter2.SetActive(true);
			MyCharacter3.SetActive(false);
		}
		else if (CharacterSelect.CurrentCharacter == 3)
		{
			MyCharacter1.SetActive(false);
			MyCharacter2.SetActive(false);
			MyCharacter3.SetActive(true);
		}
		if(lifes_count==1)
		{
			lifes[2].SetActive(false);
		}else if (lifes_count == 0)
		{
			lifes[2].SetActive(false);
			lifes[1].SetActive(false);
		}
	}
	void Update()
	{
		if(lifes_count == 2)
		{
			lifes[0].SetActive(true);
			lifes[1].SetActive(true);
			lifes[2].SetActive(true);
		}
		else if (lifes_count == 1)
		{
			lifes[2].SetActive(false);
			lifes[1].SetActive(true);
			lifes[0].SetActive(true);
		}
		else if (lifes_count == 0)
		{
			lifes[2].SetActive(false);
			lifes[1].SetActive(false);
			lifes[0].SetActive(true);
		}
	}
	public void EndGame()	
	{

		Score.runFlag = false;

		//Debug.Log("ending game");

		StartCoroutine(ExecuteAfterTime(0.5f));

		//StartCoroutine(RestartLevel());
	}
	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);

		// Code to execute after the delay
		Time.timeScale = 0f;
		if (lifes_count > 0)
		{
			Debug.Log("lifes_count = "+ lifes_count);			
			lifes_count--;
			//backToGame();
			StartCoroutine(backToGame());
		}
		else
		{
			EndGameScoreText.text = "Your Score is: " + Score.score.ToString("0");
			GameUI.SetActive(true);
		}
		

	}
	public IEnumerator backToGame()
	{		

		Debug.Log("backToGame in game manager");
		Time.timeScale = 1f / slowness;
		Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;

		yield return new WaitForSeconds(1f / slowness);

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Score.runFlag = true;
	}
	public IEnumerator RestartLevel()
	{

		Debug.Log("restart in game manager");
		Time.timeScale = 1f / slowness;
		Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;

		yield return new WaitForSeconds(1f/slowness);

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		Score.runFlag = true;
		Score.score = 0;
	}
}
