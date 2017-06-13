using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

	public float rotateSpeed = 40f;
	float tractionSpeed;
	float timer;

	public float rotationCap = 40;
	private Quaternion targetRotation;
	
	void Start () {
		targetRotation = transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		timer = Time.timeSinceLevelLoad;

		//Caps the max rotationSpeed
		if (timer >= rotationCap) {
			timer = rotationCap;
		}

		//Marble rotation that increases on change in time.
		tractionSpeed = rotateSpeed * timer * Time.deltaTime;
		transform.Rotate (new Vector3 (tractionSpeed, 0, 0));
//		playerRotation ();
	}

//	private void playerRotation () {
//		if (Input.GetKeyDown (KeyCode.RightArrow)) {
//			targetRotation *= Quaternion.AngleAxis (tractionSpeed, 0, -45);
//		}
//
//		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
//			targetRotation *= Quaternion.AngleAxis (tractionSpeed, 0, 45);
//		}
//		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 10);
//	}
}
