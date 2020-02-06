using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterSelect : MonoBehaviour
{
	public GameObject MyCharacter1;
	public GameObject MyCharacter2;
	public GameObject MyCharacter3;
	public static int CurrentCharacter;
	public InputField UserInput;
	public static string UserName;
	private TouchScreenKeyboard keyboard;
	UnityAction<DialogInterface> action;

	void Awake()
	{
		if (PlayerPrefs.GetString("UserName") != null)
		{
			UserInput.text = PlayerPrefs.GetString("UserName");
		}
	}
	// Start is called before the first frame update
	void Start()
    {
		//default character
		MyCharacter1.SetActive(true);
		
		if (MyCharacter1.activeSelf == true)
		{
			CurrentCharacter = 1;
		}
		else if(MyCharacter2.activeSelf == true)
		{
			CurrentCharacter = 2;
		}
		else if (MyCharacter3.activeSelf == true)
		{
			CurrentCharacter = 3;
		}
	}
	public void StartGame()
	{
		FindObjectOfType<AudioManager>().Play("button");
		//set the name
		UserName = UserInput.text;
		//only for debugging:
		Debug.Log("UserName: " + UserName);
		Debug.Log("UserInput.text: " + UserInput.text);
		if (UserInput.text == "")
		{
			AndroidNativeFunctions.ShowAlert("you forgot to type your name :(", "whats your name?", "ok","","", action);
			//only for debugging:
			//UserName = "Moti";
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else
		{
			PlayerPrefs.SetString("UserName", UserName);
			//move to GamePlay scene
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		
	}
	// Update is called once per frame
	void Update()
    {
        
    }

	public void CharacterChangeRight ()
	{
		if (CurrentCharacter == 1)
		{
			MyCharacter1.SetActive(false);
			MyCharacter2.SetActive(true);
			CurrentCharacter = 2;
		}
		else if(CurrentCharacter == 2)
		{
			MyCharacter2.SetActive(false);
			MyCharacter3.SetActive(true);
			CurrentCharacter = 3;
		}
		else if (CurrentCharacter == 3)
		{
			MyCharacter3.SetActive(false);
			MyCharacter1.SetActive(true);
			CurrentCharacter = 1;
		}
	}
	public void CharacterChangeLeft()
	{
		if (CurrentCharacter == 1)
		{
			MyCharacter1.SetActive(false);
			MyCharacter3.SetActive(true);
			CurrentCharacter = 3;
		}
		else if (CurrentCharacter == 2)
		{
			MyCharacter2.SetActive(false);
			MyCharacter1.SetActive(true);
			CurrentCharacter = 1;
		}
		else if (CurrentCharacter == 3)
		{
			MyCharacter3.SetActive(false);
			MyCharacter2.SetActive(true);
			CurrentCharacter = 2;
		}
	}
	void OnApplicationQuit()
	{
		FindObjectOfType<AudioManager>().Play("button");
		MyCharacter1.SetActive(true);
		MyCharacter2.SetActive(true);
		MyCharacter3.SetActive(true);

	}
	public void backToMenu()
	{
		FindObjectOfType<AudioManager>().Play("button");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
}
