using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour {

	public static Quarternion AngleAxis (float 45, Vector3.WindZo

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			transform.rotation = Quarternion.AngleAxis (45, Vector3.left);
		}
	}
}
