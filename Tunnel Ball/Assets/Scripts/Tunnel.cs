using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : DeathTrigger {

	//Speed Data
	public float speedMultiplier = 1f;
	public float secondSpeedMultiplier = 0.65f;
	public float higherVelocity = 75f;
	public float maxVelocity = 120f;
	public float startVelocity = 0;
	public float currentVelocity = 0;

	//Time
	private float timer;

	//Rotation numbers
	public int positionCounter = 1;

	//Tunnel Rotation reader
	protected GameObject glass;


	void Start () {
		ResetTunnel ();
		//starts the time tracker & sets values.
		timer = Time.timeSinceLevelLoad;
		//Object that tunnel's rotation is read from
		glass = GameObject.FindGameObjectWithTag ("Controls");
	}


	void Update () {
		tunnelRotation ();
		if (stopTunnel == false) {
			tunnelAcceleration ();
		}
	}

	//Resets Tunnel's Speed
	private void ResetTunnel () {
		currentVelocity = startVelocity;
	}


	private void tunnelRotation () {
		//Aligns all tunnel's to same rotation
		transform.rotation = glass.transform.rotation;
	}

	//Velocity of the Tunnels
	private void marbleVelocity () {
		if (stopTunnel == false) {
			if (currentVelocity < higherVelocity) {
				currentVelocity = speedMultiplier * Mathf.Pow (Time.timeSinceLevelLoad, 1.05f);
			}
			if (currentVelocity >= higherVelocity && currentVelocity < maxVelocity) {
				currentVelocity = secondSpeedMultiplier * Mathf.Pow (Time.timeSinceLevelLoad, 1.05f)+28;
			}
			if (currentVelocity >= maxVelocity) {
				currentVelocity = maxVelocity;
			}
			transform.position += -transform.forward * currentVelocity * Time.deltaTime;
		}
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
}