using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

	//Stop player controlls & Tunnel Movement

	//On & Off UI
	private GameObject player;
	private GameObject inGameUI;
	private GameObject dead;

	//Object that Tunnel's read rotation off.
	private GameObject glass;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		dead = GameObject.FindGameObjectWithTag ("Dead");
		inGameUI = GameObject.FindGameObjectWithTag ("InGame");
	}

	//Turns off Ball & Tunnel Movement
	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Player") {
			other.gameObject.SetActive (false);

			//Turns restart UI on
			inGameUI.gameObject.SetActive (false);
			dead.transform.GetChild (0).gameObject.SetActive (true);

			//Deactivates Tunnel movement
//			glass.GetComponent<GlassRotate>().enabled = false;
		}
	}
}
