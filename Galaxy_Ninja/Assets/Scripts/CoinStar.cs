using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStar : MonoBehaviour
{
	public float fallSpeed = 0.5f;
	// Start is called before the first frame update
	void Start()
    {
		//Physics.gravity = new Vector3(0, -1.0F, 0);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.name.Equals("astro-ninja") || collision.name.Equals("astro-ninja-gold") || collision.name.Equals("ai-ninja"))
		{
			FindObjectOfType<AudioManager>().Play("coin");
			Score.score += 50;
			Destroy(gameObject);
		}
	}
	// Update is called once per frame
	void Update()
	{
		transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
		if (transform.position.y < -9f)
		{
			Destroy(gameObject);
		}
	}
}
