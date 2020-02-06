using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFingerMove : MonoBehaviour
{
	private Vector3 touchPosition;
	private Rigidbody2D rb;
	private Vector3 direction;
	private float moveSpeed = 40f;
	public static bool shield = false;
	public static int timeToShield;
	private Color originalColor;
	public static int collisionTime;

	// Start is called before the first frame update
	void Start()
    {
		originalColor = this.GetComponent<Renderer>().material.color;
		rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if (shield)
		{			
			this.GetComponent<Renderer>().material.SetColor("_Color", Color.Lerp(Color.magenta, Color.yellow, 0.3f));
			//transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
			rb.freezeRotation = true;			
			//this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
		else if (!shield)
		{
			this.GetComponent<Renderer>().material.color = originalColor;
			rb.freezeRotation = false;
		}

		if (Time.time > timeToShield)
		{
			shield = false;
		}
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			//Debug.Log("*************touch.position: " + touch.position);
			touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			touchPosition.z = 0;
			direction = (touchPosition - transform.position);
			rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;
			//Debug.Log("velocity= " + rb.velocity.ToString() + "time= " + Time.time.ToString());

			if (touch.phase == TouchPhase.Ended)
				rb.velocity = Vector2.zero;
		}
    }

	void OnCollisionEnter2D()
	{
		if (!shield)
		{
			if (PlayerPrefs.GetInt("VBR") == 1)
			{
				AndroidNativeFunctions.Vibrate(500);
			}
			Debug.Log("we got hit");
			FindObjectOfType<AudioManager>().Play("collision");
			FindObjectOfType<GameManager>().EndGame();
			collisionTime = (int)Time.time;
		}
	}
}
