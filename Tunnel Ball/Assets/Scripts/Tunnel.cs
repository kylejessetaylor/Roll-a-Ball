using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour {

	//Speed Data
	public float speedMultiplier = 0.02f;
	public float maxVelocity = 1.5f;
	public float startSpeed = 0;
	public float velocity = 0;

	//Time
	private float timer;

	//Rotation numbers
	public int positionCounter = 1;

	//Smooth Rotation
	public float rotationSpeed = 10;
	private Quaternion targetRotation;




	void Start () {
		ResetTunnel ();
		//starts the time tracker
		timer = Time.timeSinceLevelLoad;
		targetRotation = transform.rotation;
	}


	void Update () {
		tunnelRotation ();
		tunnelAcceleration ();
	}

	//Resets Tunnel's Speed
	private void ResetTunnel () {
		velocity = startSpeed;
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

//	//Velocity of the Tunnels
//	private void marbleVelocity () {
//		if (currentSpeed < maxSpeed) {
//			currentSpeed = speedMultiplier * Mathf.Pow (Time.timeSinceLevelLoad, 1.05f);
//		}
//		if (currentSpeed >= maxSpeed) {
//			currentSpeed = maxSpeed;
//		}
//		transform.position += -transform.forward * currentSpeed;    
//	}
//
//	//Acceleration of the Tunnels
//	private void tunnelAcceleration () {
//		//Velocity increase per sec.
//		if (Time.timeSinceLevelLoad - timer >= 0.5f) {
//			marbleVelocity ();
//			timer = Time.timeSinceLevelLoad;
//		} else {
//			transform.position += -transform.forward * currentSpeed;
//		}
//	}

	private void tunnelAcceleration () {
		if (velocity < maxVelocity) {
			velocity = speedMultiplier * Mathf.Pow (Time.timeSinceLevelLoad, 1.05f);
		}
		if (velocity >= maxVelocity) {
			velocity = maxVelocity;
		}

		transform.position += -transform.forward * velocity;
	}
}