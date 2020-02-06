using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	private Transform targetToFollow;

	// Update is called once per frame
	void Update()
    {
		transform.position = new Vector3(
			transform.position.x,
			targetToFollow.position.y,
			transform.position.z);
    }
}
