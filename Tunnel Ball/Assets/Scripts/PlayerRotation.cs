using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

	public float rotateSpeed = 0.8f;
	float tractionSpeed;
	float timer;

	public float rotationCap = 40;
	
	// Update is called once per frame
	void Update () {
		timer = Time.timeSinceLevelLoad;

		//Caps the max rotationSpeed
		if (timer >= rotationCap) {
			timer = rotationCap;
		}

		//Marble rotation that increases on change in time.
		tractionSpeed = rotateSpeed * timer;
		transform.Rotate (new Vector3 (tractionSpeed, 0, 0));
	}
}
