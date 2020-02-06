using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
	public Toggle VBR_toggle;
	public Toggle Sound_toggle; 
	void Awake()
	{
		//Debug.Log("Playerprefs of VBR = " + PlayerPrefs.GetInt("VBR").ToString());
		//vibration Setection
		if (PlayerPrefs.GetInt("VBR") == 1)
		{
			VBR_toggle.isOn = true;
		}
		else
		{
			VBR_toggle.isOn = false;
		}
		//sound detection
		if (PlayerPrefs.GetInt("SND") == 1)
		{
			Sound_toggle.isOn = true;
		}
		else
		{
			Sound_toggle.isOn = false;
		}
	}
	// Start is called before the first frame update
	void Start()
    {		
		

	}

    // Update is called once per frame
    void Update()
    {

	}
	public void backToMenu()
	{
		FindObjectOfType<AudioManager>().Play("button");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
	}
	public void soundChange()
	{
		PlayerPrefs.SetInt("SND", (Sound_toggle.isOn.ToString() == "True") ? 1 : 0);

		if (PlayerPrefs.GetInt("SND") == 1)
		{
			FindObjectOfType<AudioManager>().sounds[0].volume = 0.5f;
			FindObjectOfType<AudioManager>().sounds[1].volume = 0.8f;
			FindObjectOfType<AudioManager>().sounds[2].volume = 0.8f;
			FindObjectOfType<AudioManager>().sounds[3].volume = 0.5f;
			FindObjectOfType<AudioManager>().Play("themeMusic");
		}
		else
		{
			FindObjectOfType<AudioManager>().sounds[0].volume = 0;
			FindObjectOfType<AudioManager>().sounds[1].volume = 0;
			FindObjectOfType<AudioManager>().sounds[2].volume = 0;
			FindObjectOfType<AudioManager>().sounds[3].volume = 0;
			FindObjectOfType<AudioManager>().Play("themeMusic");
		}
	}


	public void vibrationChange()
	{
		PlayerPrefs.SetInt("VBR", (VBR_toggle.isOn.ToString()=="True")? 1:0);
		Debug.Log("VBR_toggle after vibration change: " + VBR_toggle.isOn.ToString());
	}
}
