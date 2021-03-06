﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
	//Speed Data
	public float speedMultiplier = 1f;
	public float maxVelocity = 120f;
    public float initialSpeed;

    public float startVelocity = 0;
	public float currentVelocity = 0;

    private bool atMaxVelocity = false;

    //Time
    private float timer;

	//Rotation numbers
	public int positionCounter = 1;

	//Tunnel Rotation reader
	protected GameObject rotator;

    //Mobile changes in code
    public GameObject manager;
    private float mobileMultiplier;

    void Start () {
		ResetTunnel ();
		//starts the time tracker & sets values.
		timer = Time.timeSinceLevelLoad;
		//Object that tunnel's rotation is read from
		rotator = GameObject.FindGameObjectWithTag ("Controls");

        //Speed Multiplier for Mobile
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            mobileMultiplier = manager.GetComponent<Manager>().mobileMultiplier;
            speedMultiplier = speedMultiplier * mobileMultiplier;
        }
    }

	void LateUpdate () {
		TunnelRotation ();
		TunnelAcceleration ();
    }

	//Resets Tunnel's Speed
	private void ResetTunnel () {
		currentVelocity = startVelocity;
	}

	private void TunnelRotation () {
		//Aligns all tunnel's to same rotation
		transform.rotation = rotator.transform.rotation;
	}

	//Velocity of the Tunnels
	private void MarbleVelocity ()
    {
        //Velocity, Circular Formula
        if (currentVelocity < maxVelocity && atMaxVelocity == false)
        {
            //Radius of the circle
            float radiusSquared = Mathf.Pow(maxVelocity, 2f);
            //Calculates y^2
            float ySquared = radiusSquared - Mathf.Pow(Time.timeSinceLevelLoad - maxVelocity + initialSpeed, 2f);
            //Current Velocity with multiplier (increasing multiplier increases rate of speed);
            currentVelocity = speedMultiplier * 0.1f * ySquared;
        }
        else
        {
            atMaxVelocity = true;
            currentVelocity = maxVelocity;
        }
        //Transform Forward
        transform.position -= transform.forward * currentVelocity * Time.deltaTime;
	}

	//Acceleration of the Tunnels
	private void TunnelAcceleration () {
		//Velocity increase per sec.
		if (Time.timeSinceLevelLoad - timer >= 0.01f) {
			MarbleVelocity ();
			timer = Time.timeSinceLevelLoad;
		} else {
			transform.position += -transform.forward * currentVelocity * Time.deltaTime;
		}
	}

    #region TunnelDespawn
    //Despawns tunnel that player has passed through and puts it back into the object pool
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Despawn")
        {
            TrashMan.despawn(gameObject);
        }
    }
    #endregion

}