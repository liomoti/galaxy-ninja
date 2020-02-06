using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteorite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
