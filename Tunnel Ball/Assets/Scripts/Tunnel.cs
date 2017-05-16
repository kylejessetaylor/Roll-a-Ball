using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour {

	//Speed Data
	public float speedMultiplier = 0.015f;
	public float maxSpeed = 70;
	public float startSpeed = 0;
	public float currentSpeed = 0;

	//Time
	private float timer;


	//Stored info on where the Marble (Tunnel) starts.
	//public Quaternion startingRotation;
	//public Vector3 startingPosition;

	void Start () {
		ResetTunnel ();

		//starts the time tracker
		timer = Time.time;
	}


	void Update () {
		tunnelRotation ();
		//Velocity increase per sec.
		if (Time.time - timer >= 0.5f) {
			marbleVelocity ();
			timer = Time.time;
		} else {
			transform.position += -transform.forward * currentSpeed;
		}
	}

	private void ResetTunnel () {
		//transform.position = startingPosition;
		//transform.rotation = startingRotation;
		currentSpeed = startSpeed;
	}
		
	private void tunnelRotation () {
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			transform.Rotate(Vector3.back * 45);

		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			transform.Rotate(Vector3.forward * 45);
		}
	}

	private void marbleVelocity () {
		if (currentSpeed < maxSpeed) {
			currentSpeed = speedMultiplier * Mathf.Pow (Time.time, 1.1f);
		}
		if (currentSpeed >= maxSpeed) {
			currentSpeed = maxSpeed;
		}
		transform.position += -transform.forward * currentSpeed;    
	}
		
}
