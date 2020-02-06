using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
	//[SerializeField]
	public string name { get; set; }
	//[SerializeField]
	public string score { get; set; }

	public Data(string name, string score)
	{
		this.name = name;
		this.score = score;
	}
}
