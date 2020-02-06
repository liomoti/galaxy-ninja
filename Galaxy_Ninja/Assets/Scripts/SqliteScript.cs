using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.SceneManagement;

public class SqliteScript : MonoBehaviour
{
	public GameObject[] ScoreRank;
	public GameObject[] ScoreName;
	public GameObject[] ScoreDate;
	public GameObject[] ScoreScore;

	// Start is called before the first frame update
	void Start()
    {
		int rank = 0;
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

		// Read and print all values in table
		IDbCommand cmnd_read = dbcon.CreateCommand();	
		string query = "SELECT * FROM myscore_table ORDER BY score DESC";
		cmnd_read.CommandText = query;
		reader = cmnd_read.ExecuteReader();

		while (reader.Read() && rank <10)
		{
			Debug.Log("id: " + reader[0].ToString());
			Debug.Log("name: " + reader[1].ToString());
			Debug.Log("date: " + reader[2].ToString());
			Debug.Log("score: " + reader[3].ToString());
			ScoreRank[rank].GetComponent<UnityEngine.UI.Text>().text = (rank + 1).ToString();
			ScoreName[rank].GetComponent<UnityEngine.UI.Text>().text = reader[1].ToString();
			ScoreDate[rank].GetComponent<UnityEngine.UI.Text>().text = reader[2].ToString();
			ScoreScore[rank].GetComponent<UnityEngine.UI.Text>().text = reader[3].ToString();
			rank++;
		}

		// Close connection
		dbcon.Close();

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public static void EnterScore(string name, int score)
	{

		//save the score in the DB
		//delete the last score in the table

		//get current date
		string date = System.DateTime.Now.ToString("dd/MM/yyyy");
		// Create database
		string connection2 = "URI=file:" + Application.persistentDataPath + "/Ninja_DB";
		// Open connection
		IDbConnection dbcon2 = new SqliteConnection(connection2);
		dbcon2.Open();

		// Insert values in table
		IDbCommand cmnd2 = dbcon2.CreateCommand();
		cmnd2.CommandText = "INSERT INTO myscore_table (name, date, score) VALUES (\""+name+"\",\""+date+"\", \""+score+"\")";
		cmnd2.ExecuteNonQuery();
		//remove score that not in the greatest 8
		cmnd2.CommandText = "delete from myscore_table where id not in (select id from myscore_table order by score DESC limit 8)";
		cmnd2.ExecuteNonQuery();

		// Close connection
		dbcon2.Close();
	}
	public void backToMenu()
	{
		FindObjectOfType<AudioManager>().Play("button");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
	}
}
