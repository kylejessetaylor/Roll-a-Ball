using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathTrigger : MonoBehaviour {


	//On & Off UI
	private GameObject player;
	public GameObject deadPlayer;

	private GameObject inGameUI;
	private GameObject dead;

	//Object that Tunnel's read rotation off & tunnel movement.
	private GameObject glass;
	private GameObject[] tunnels;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");

		dead = GameObject.FindGameObjectWithTag ("Dead");
		inGameUI = GameObject.FindGameObjectWithTag ("InGame");

		glass = GameObject.FindGameObjectWithTag ("Controls");
	}

	//Turns off Ball & Tunnel Movement
	void OnTriggerEnter(Collider other) {
		tunnels = GameObject.FindGameObjectsWithTag ("Tunnels");	
		if (other.tag == "Obstacle") {
			//Turns off Player
			player.gameObject.SetActive (false);
			Instantiate(deadPlayer);

			//Turns restart UI on
			inGameUI.gameObject.SetActive (false);
			dead.transform.GetChild (0).gameObject.SetActive (true);

			//Deactivates Tunnel movement
			Cursor.visible = true;
			glass.GetComponent<GlassRotate>().enabled = false;
			foreach (GameObject tunnel in tunnels) {	
				tunnel.GetComponent<Tunnel> ().enabled = false;
			}
		}
	}
}
