using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public Transform player;
	public Text scoreText;
	public GameObject levelUp;
	public Transform levelUpSpawn;
	public static float score = 0;
	public static bool runFlag = true;
	

	// Update is called once per frame
	void Update()
    {
		if (runFlag)
		{
			score = score + 0.1f;
		}
		scoreText.text = "score: " + score.ToString("0") + "   " + CharacterSelect.UserName;
		if ((int)score > 700 && start_menu.level1)
		{
			Destroy(Instantiate(levelUp, levelUpSpawn.position, Quaternion.identity).gameObject, 1);
			start_menu.level1 = false;
		}
		if ((int)score > 1500 && start_menu.level2)
		{
			Destroy(Instantiate(levelUp, levelUpSpawn.position, Quaternion.identity).gameObject, 1);
			start_menu.level2 = false;
		}
		if ((int)score > 3000 && start_menu.level3)
		{
			Destroy(Instantiate(levelUp, levelUpSpawn.position, Quaternion.identity).gameObject, 1);
			start_menu.level3 = false;
		}
		//if (level1)
		//{
		//	level1 = false;
		//	Destroy(Instantiate(levelUp, levelUpSpawn.position, Quaternion.identity).gameObject, 2);
		//}
	}
}
