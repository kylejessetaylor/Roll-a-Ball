﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfRotate : MonoBehaviour {

	public float speed = 0.1f;

	Vector3 from = new Vector3 (0f, -115f, 0f);
	Vector3 to = new Vector3 (0f, 115f, 0f);

	void Update () {
		float t = Mathf.PingPong (Time.time * speed * 2.0f, 1.0f);

		//Is likely rotating X and Z when tunnel rotates.
		transform.localEulerAngles = Vector3.Lerp (from, to, t);
	}
}