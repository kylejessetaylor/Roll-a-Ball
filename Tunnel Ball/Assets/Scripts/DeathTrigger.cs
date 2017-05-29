using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

	//Stop player controlls & Tunnel Movement

	//On & Off UI
	private GameObject youDeadUI;
	private GameObject inGameUI;


	void Start () {
		youDeadUI = GameObject.FindGameObjectWithTag ("YouDead");
		inGameUI = GameObject.FindGameObjectWithTag ("InGame");
	}

	//Turns off Ball & Tunnel Movement
	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Player") {
			other.gameObject.SetActive (false);
			//Turns death UI on
			youDeadUI.gameObject.SetActive (true);
			inGameUI.gameObject.SetActive (false);
			//Deactivates Tunnel movement
//			otherObject.GetComponent<NameOfScript>().enabled = false;
		}
	}
}
