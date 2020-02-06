using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class start_menu : MonoBehaviour
{
	public static string min_score_token;
	public static string min_score = "999999";
	private DatabaseReference fireDbRef_minscore;
	public static bool level1 = true;
	public static bool level2 = true;
	public static bool level3 = true;
	public void StartGame()
	{
		FindObjectOfType<AudioManager>().Play("button");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void MyScoreboard()
	{
		FindObjectOfType<AudioManager>().Play("button");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
	}
	public void GlobalScoreboard()
	{
		FindObjectOfType<AudioManager>().Play("button");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
	}
	public void GameSettings()
	{
		FindObjectOfType<AudioManager>().Play("button");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
	}
	public void QuitGame()
	{
		FindObjectOfType<AudioManager>().Play("button");
		Application.Quit();
	}
	void Start()
	{
		//get min score from firebase
		GetMinScore();

		// Create database
		string connection = "URI=file:" + Application.persistentDataPath + "/Ninja_DB";
		// Open connection
		IDbConnection dbcon = new SqliteConnection(connection);
		dbcon.Open();

		// Create table
		IDbCommand dbcmd;
		IDataReader reader;

		dbcmd = dbcon.CreateCommand();
		string q_createTable =
		  "CREATE TABLE IF NOT EXISTS myscore_table (id INTEGER PRIMARY KEY, name STRING, date STRING, score INTEGER )";

		dbcmd.CommandText = q_createTable;
		reader = dbcmd.ExecuteReader();

		// Close connection
		dbcon.Close();
	}
	// Update is called once per frame
	void Update()
    {
        
    }
	public void GetMinScore()
	{		
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://galaxyninja-330d8.firebaseio.com/");
		fireDbRef_minscore = FirebaseDatabase.DefaultInstance.RootReference;
		FirebaseDatabase.DefaultInstance.GetReference("score/").GetValueAsync().ContinueWith(task =>
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
						if (ds.Key.Equals("min_score"))
						{
							min_score = ds.Value.ToString();
							Debug.Log("min_score= " + min_score);
							//PlayerPrefs.SetString("min_score", min_score);
						}
						else
						{
							min_score_token = ds.Value.ToString();
							Debug.Log("token= " + ds.Value.ToString());
							//PlayerPrefs.SetString("min_score_token", min_score_token);
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
}
