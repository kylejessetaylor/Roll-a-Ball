using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullRotate : MonoBehaviour {

	public float rotateSpeed = 30;

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, rotateSpeed, 0) * Time.deltaTime);
	}
}
