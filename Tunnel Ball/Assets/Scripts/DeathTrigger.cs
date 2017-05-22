//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class DeathTrigger : MonoBehaviour {
//
//	public GameObject tunnelScript;
//	//On & Off UI
//	public GameObject youDeadUI;
//	public GameObject inGameUI;
//
////	public GameObject Tunnelname;
//
//	//Turns off Ball & Tunnel Movement
//	void OnTriggerStay(Collider other) {
//		Debug.Log ("Ded");
//
//		if (other.tag == "Player") {
//			other.gameObject.SetActive (false);
//			youDeadUI.gameObject.SetActive (true);
//			inGameUI.gameObject.SetActive (false);
////			otherObject.GetComponent<NameOfScript>().enabled = false;
//		}
//	}
//}
