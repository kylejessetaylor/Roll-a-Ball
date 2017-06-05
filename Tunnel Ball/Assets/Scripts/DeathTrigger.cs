using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {


	//On & Off UI
	private GameObject player;
	private GameObject inGameUI;
	private GameObject dead;

	//Object that Tunnel's read rotation off & tunnel movement.
	private GameObject glass;
	private GameObject[] tunnels;
	public bool stopTunnel = false;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		dead = GameObject.FindGameObjectWithTag ("Dead");
		inGameUI = GameObject.FindGameObjectWithTag ("InGame");
		glass = GameObject.FindGameObjectWithTag ("Controls");
		tunnels = GameObject.FindGameObjectsWithTag ("Tunnels");
	}

	//Turns off Ball & Tunnel Movement
	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Obstacle") {
			player.gameObject.SetActive (false);

			//Turns restart UI on
			inGameUI.gameObject.SetActive (false);
			dead.transform.GetChild (0).gameObject.SetActive (true);

			//Deactivates Tunnel movement
			stopTunnel = true;
			Cursor.visible = true;
//			glass.GetComponent<GlassRotate>().enabled = false;
//			foreach (GameObject tunnel in tunnels) {	
//				tunnel.GetComponent<Tunnel> ().enabled = false;
//			}
		}
	}
}
