using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullRotate : MonoBehaviour {

	public float rotateSpeed;

	public float maxRotateSpeed = 60;
	public float startRotateSpeed = 25;
	//	public float rotateSpeedMultiplier = 0.6f;

	// Update is called once per frame
	void Update () {
		if (rotateSpeed >= maxRotateSpeed) {
			rotateSpeed = maxRotateSpeed;
		} else {
			rotateSpeed = 0.6f * Time.time + startRotateSpeed;
		}

		transform.Rotate (new Vector3 (0, rotateSpeed, 0) * Time.deltaTime);
	}	
}
