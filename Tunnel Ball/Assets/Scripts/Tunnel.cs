using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour {

	//Speed Data
	public float speedMultiplier = 0.02f;
	public float maxVelocity = 1.5f;
	public float startVelocity = 0;
	public float currentVelocity = 0;

	//Time
	private float timer;

	//Rotation numbers
	public int positionCounter = 1;

	//Smooth Rotation
	public float rotationSpeed = 10;
	private Quaternion targetRotation;

	//Tunnel Rotation reader
//	[HideInInspector]
//	public GameObject glassRotation;


	void Start () {
		ResetTunnel ();
		//starts the time tracker & sets values.
		timer = Time.timeSinceLevelLoad;
		targetRotation = transform.rotation;
//		glassRotation = tag
	}


	void Update () {
		tunnelRotation ();
		tunnelAcceleration ();
	}

	//Resets Tunnel's Speed
	private void ResetTunnel () {
		currentVelocity = startVelocity;
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

	//Velocity of the Tunnels
	private void marbleVelocity () {
		if (currentVelocity < maxVelocity) {
			currentVelocity = speedMultiplier * Mathf.Pow (Time.timeSinceLevelLoad, 1.05f);
		}
		if (currentVelocity >= maxVelocity) {
			currentVelocity = maxVelocity;
		}
		//Stops Tunnel's rotation & movement if DeathTrigger activates.
//		potentialStop ();
		transform.position += -transform.forward * currentVelocity;
	}

	//Acceleration of the Tunnels
	private void tunnelAcceleration () {
		//Velocity increase per sec.
		if (Time.timeSinceLevelLoad - timer >= 0.01f) {
			marbleVelocity ();
			timer = Time.timeSinceLevelLoad;
		} else {
			transform.position += -transform.forward * currentVelocity;
		}
	}

	//Stops Tunnel's rotation & movement if DeathTrigger activates.
//	private void potentialStop () {
//		if (Time.timeScale = 0) {
//		transform.position = transform.position;
//
//		} else {
//			transform.position += -transform.forward * currentSpeed;
//		}
//	}
		
}