using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassRotate : DeathTrigger {

	//Smooth Rotation
	public float rotationSpeed = 10;
	private Quaternion targetRotation;

	//Player's corpse spawn
	private GameObject corpse;

	void Start () {
		targetRotation = transform.rotation;
		corpse = GameObject.FindGameObjectWithTag ("Corpse");
	}

	// Update is called once per frame
	void Update () {
		tunnelRotation ();
		PlayerCorpse ();
	}

	//Rotates Tunnel smoothly
	private void tunnelRotation () {
		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
				targetRotation *= Quaternion.AngleAxis (45, Vector3.back);
			}

		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
				targetRotation *= Quaternion.AngleAxis (45, Vector3.forward);
			}
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
	}

	//Turns on Player Corpse when death trigger activates
	private void PlayerCorpse () {
		if (GameObject.FindGameObjectWithTag ("Player") == null && GameObject.FindGameObjectWithTag ("Shards") == null) {
			corpse.transform.GetChild (0).gameObject.SetActive (true);
		}
	}
}
