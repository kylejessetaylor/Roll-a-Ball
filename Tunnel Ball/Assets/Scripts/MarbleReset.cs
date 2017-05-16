using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleReset : MonoBehaviour {

	//Stored info on where the Marble (Tunnel) starts.
	public Quaternion startingRotation;
	public Vector3 startingPosition;

	void Start () {
		ResetMarble ();
	}
		
	
	// Update is called once per frame
	void Update () {
		
	}

	private void ResetMarble () {
		transform.position = startingPosition;
		transform.rotation = startingRotation;
	}
}
