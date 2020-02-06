using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Proyecto26;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using UnityEngine.UI;

public class GlobalScoreScript : MonoBehaviour
{
	public GameObject[] ScoreRank;
	public GameObject[] ScoreName;
	public GameObject[] ScoreScore;
	public static bool incomeData = false;
	//public static GlobalScoreScript Instance;
	private Dictionary<string, int> myList = new Dictionary<string, int>();	
	private Dictionary<string, int> showMyList = new Dictionary<string, int>();
	private DatabaseReference fireDbRef;
	
	// Start is called before the first frame update
	void Start()
	{
		//Data tempData = new Data("eldar", "900");
		//RestClient.Post("https://galaxyninja-330d8.firebaseio.com/users/", tempData).Then(res =>
		//{
		//	Debug.Log("status: " + res.StatusCode.ToString());
		//});
		showMyList.Clear();

		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://galaxyninja-330d8.firebaseio.com/");
		fireDbRef = FirebaseDatabase.DefaultInstance.RootReference;

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
						bool flag1 = false;
						bool flag2 = false;
						string str_token = ds.Key;
						string str_score = "";
						string str_name = "";
						foreach (DataSnapshot dsinto in ds.Children)
						{
							incomeData = true;
							if (dsinto.Key.Equals("score"))
							{
								flag1 = true;
								str_score = dsinto.Value.ToString();
								//insert into dictionary

							}
							if (dsinto.Key.Equals("name"))
							{
								flag2 = true;
								str_name = dsinto.Value.ToString();
							}
							if (flag1 && flag2)
							{
								showMyList.Add(str_token + "-" + str_name, int.Parse(str_score));
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Debug.Log(e.ToString());
			}
		});
	}

	// Update is called once per frame
	void Update()
	{
		//sort the dictionary
		List<KeyValuePair<string, int>> mySortList = new List<KeyValuePair<string, int>>(showMyList);
		mySortList.Sort(
			delegate (KeyValuePair<string, int> firstPair,
			KeyValuePair<string, int> nextPair)
			{
				return firstPair.Value.CompareTo(nextPair.Value);
			}
		);
		mySortList.Reverse();
		//show all the scores on table
		int rank = 0;
		foreach (KeyValuePair<string, int> kvs in mySortList)
		{
			ScoreRank[rank].GetComponent<UnityEngine.UI.Text>().text = (rank + 1).ToString();
			string[] words = kvs.Key.Split('-');
			ScoreName[rank].GetComponent<UnityEngine.UI.Text>().text = words[1];
			ScoreScore[rank].GetComponent<UnityEngine.UI.Text>().text = kvs.Value.ToString();
			rank++;
			if (rank == 8)
			{
				break;
			}
		}
	}
	//get all the scores to fill the global score table
	private void showAllScores()
	{		

	}
	
	
	public void backToMenu()
	{
		FindObjectOfType<AudioManager>().Play("button");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 5);
	}
}
