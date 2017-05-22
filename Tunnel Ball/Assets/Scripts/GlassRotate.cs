using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassRotate : MonoBehaviour {

	//Smooth Rotation
	public float rotationSpeed = 10;
	private Quaternion targetRotation;

	void Start () {
		targetRotation = transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		tunnelRotation ();
	}

	//Rotates Tunnel smoothly
	private void tunnelRotation () {
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			targetRotation *= Quaternion.AngleAxis (45, Vector3.back);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			targetRotation *= Quaternion.AngleAxis (45, Vector3.forward);
		}
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
	}
}
