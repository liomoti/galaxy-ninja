using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using System;
using System.Linq;
using Newtonsoft.Json;

public class EndGameS : MonoBehaviour
{
	public float slowness = 10f;
	private DatabaseReference DbRef;
	private Dictionary<string, int> myList = new Dictionary<string, int>();
	private Dictionary<string, int> showMyList = new Dictionary<string, int>();
	void Start()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://galaxyninja-330d8.firebaseio.com/");
		DbRef = FirebaseDatabase.DefaultInstance.RootReference;
	}
	public void RestartL()
	{
		FindObjectOfType<AudioManager>().Play("button");
		SqliteScript.EnterScore(CharacterSelect.UserName, (int)Score.score); //save score in sqlite
		SaveScoreFire(CharacterSelect.UserName, ((int)Score.score).ToString()); //save score in firebase
		GameManager.lifes_count = 2; //reset lifes
		StartCoroutine(RestartLevel());
	}
	public void backTM()
	{
		FindObjectOfType<AudioManager>().Play("button");
		SqliteScript.EnterScore(CharacterSelect.UserName, (int)Score.score); //save score in sqlite
		SaveScoreFire(CharacterSelect.UserName, ((int)Score.score).ToString()); //save score in firebase
		GameManager.lifes_count = 2; //reset lifes
		StartCoroutine(BackToMenu());
	}

	//save the score in firebase
	public void SaveScoreFire(string myName, string myScore)
	{
		Debug.Log("SaveScoreFire()");
		Data myData = new Data(myName, myScore);
		string randomStr = Guid.NewGuid().ToString("n").Substring(0, 8);
		//check if my score is above min score
		if (int.Parse(myScore) > int.Parse(start_menu.min_score))
		{
			Debug.Log("myScore > min_score");
			//save my score
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://galaxyninja-330d8.firebaseio.com/");
			DbRef = FirebaseDatabase.DefaultInstance.RootReference;
			//string json_data = JsonUtility.ToJson(myData);
			string json_data = JsonConvert.SerializeObject(myData);
			//write data to database
			DbRef.Child("users").Child(randomStr).SetRawJsonValueAsync(json_data);
			//delete the last score
			DbRef.Child("users").Child(start_menu.min_score_token).RemoveValueAsync();
			//update min score in firebase
			UpdateMinScore();


		}
	}
	//update the min_score value in firebase
	public void UpdateMinScore()
	{
		FirebaseDatabase.DefaultInstance.GetReference("users/").GetValueAsync().ContinueWith(task =>
		{
			try
			{
				if (task.IsFaulted)
				{
					Debug.Log("task error");
					// some error
				}
				else if (task.IsCompleted)
				{
					DataSnapshot snapshot = task.Result;
					foreach (DataSnapshot ds in snapshot.Children)
					{
						string str_token = ds.Key;
						foreach (DataSnapshot dsinto in ds.Children)
						{
							if (dsinto.Key.Equals("score"))
							{
								string str_score = dsinto.Value.ToString();
								//insert into dictionary
								myList.Add(str_token, int.Parse(str_score));
							}
						}
					}
					//sort the dictionary
					List<KeyValuePair<string, int>> mySortList = new List<KeyValuePair<string, int>>(myList);
					mySortList.Sort(
						delegate (KeyValuePair<string, int> firstPair,
						KeyValuePair<string, int> nextPair)
						{
							return firstPair.Value.CompareTo(nextPair.Value);
						}
					);
					//get the min value - update min_score_token in firebase
					string getOutToken = mySortList[0].Key;
					string getOutScore = mySortList[0].Value.ToString();
					DbRef.Child("score").Child("token").SetValueAsync(getOutToken);
					DbRef.Child("score").Child("min_score").SetValueAsync(getOutScore);
				}
			}
			catch (Exception e)
			{
				Debug.Log(e.ToString());
			}
		});
	}
	public IEnumerator BackToMenu()
	{
		Time.timeScale = 1f / slowness;
		Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;

		yield return new WaitForSeconds(1f / slowness);

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);

		Score.runFlag = true;
		Score.score = 0;
	}

	public IEnumerator RestartLevel()
	{
		Debug.Log("restart in endgameS");
		Time.timeScale = 1f / slowness;
		Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;

		yield return new WaitForSeconds(1f / slowness);	

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		Score.runFlag = true;
		Score.score = 0;
	}

	public void QuitGame()
	{
		FindObjectOfType<AudioManager>().Play("button");
		Application.Quit();
	}
}
