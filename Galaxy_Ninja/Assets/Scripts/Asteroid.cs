using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	

	void Start ()
	{
		GetComponent<Rigidbody2D>().gravityScale += Time.timeSinceLevelLoad / 40f;
	}

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -9f)
		{
			Destroy(gameObject);
		}
    }
}
