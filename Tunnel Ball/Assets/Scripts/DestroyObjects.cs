using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
			Destroy (other.gameObject);
	}
}
