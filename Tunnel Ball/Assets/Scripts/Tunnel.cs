using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : DeathTrigger {

	//Speed Data
	public float speedMultiplier = 0.02f;
	public float maxVelocity = 1.5f;
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
			if (currentVelocity <= maxVelocity) {
				currentVelocity = speedMultiplier * Mathf.Pow (Time.timeSinceLevelLoad, 1.05f);
			}
			if (currentVelocity >= maxVelocity) {
				currentVelocity = maxVelocity;
			}
			transform.position += -transform.forward * currentVelocity;
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