using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnTunnel : MonoBehaviour {

	//Despawns tunnel that player has passed through and puts it back into the object pool
	void OnTriggerEnter (Collider other) {
		TrashMan.despawn (other.gameObject);
	}
}
