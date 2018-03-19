using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{

	//Speed Data
	public float speedMultiplier = 1f;
	//public float secondSpeedMultiplier = 0.65f;
	//public float higherVelocity = 75f;
	public float maxVelocity = 120f;
	public float startVelocity = 0;
	public float currentVelocity = 0;

    private bool atMaxVelocity = false;

    //Time
    private float timer;

	//Rotation numbers
	public int positionCounter = 1;

	//Tunnel Rotation reader
	protected GameObject rotator;


	void Start () {
		ResetTunnel ();
		//starts the time tracker & sets values.
		timer = Time.timeSinceLevelLoad;
		//Object that tunnel's rotation is read from
		rotator = GameObject.FindGameObjectWithTag ("Controls");
	}


	void Update () {
		tunnelRotation ();
		tunnelAcceleration ();
	}

	//Resets Tunnel's Speed
	private void ResetTunnel () {
		currentVelocity = startVelocity;
	}


	private void tunnelRotation () {
		//Aligns all tunnel's to same rotation
		transform.rotation = rotator.transform.rotation;
	}

	//Velocity of the Tunnels
	private void marbleVelocity ()
    {

        //Velocity, Multiple Linear Formulas
        //if (currentVelocity < higherVelocity) {
        //	currentVelocity = speedMultiplier * Mathf.Pow (Time.timeSinceLevelLoad, 1.05f);
        //}
        //if (currentVelocity >= higherVelocity && currentVelocity < maxVelocity) {
        //	currentVelocity = secondSpeedMultiplier * Mathf.Pow (Time.timeSinceLevelLoad, 1.05f)+28;
        //}
        //if (currentVelocity >= maxVelocity) {
        //	currentVelocity = maxVelocity;
        //}

        //Velocity, Circular Formula
        if (currentVelocity < maxVelocity && atMaxVelocity == false)
        {
            //Radius of the circle
            float radiusSquared = Mathf.Pow(maxVelocity, 2f);
            //Calculates y^2
            float ySquared = radiusSquared - Mathf.Pow(Time.timeSinceLevelLoad - maxVelocity, 2f);
            //Current Velocity with multiplier (increasing multiplier increases rate of speed);
            currentVelocity = speedMultiplier * 0.1f * ySquared;
        }
        else
        {
            atMaxVelocity = true;
            currentVelocity = maxVelocity;
        }

        transform.position += -transform.forward * currentVelocity * Time.deltaTime;
	}

	//Acceleration of the Tunnels
	private void tunnelAcceleration () {
		//Velocity increase per sec.
		if (Time.timeSinceLevelLoad - timer >= 0.01f) {
			marbleVelocity ();
			timer = Time.timeSinceLevelLoad;
		} else {
			transform.position += -transform.forward * currentVelocity * Time.deltaTime;
		}
	}
    #region TunnelDespawn
    //Despawns tunnel that player has passed through and puts it back into the object pool
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Despawn Tunnel")
        {
            TrashMan.despawn(gameObject);
        }
    }
    #endregion
}