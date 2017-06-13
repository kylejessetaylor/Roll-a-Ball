using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardImpulse : MonoBehaviour {

//	private GameObject[] shards;
	public Rigidbody rb;
	public float multiplier = 1f;
	public float thrust;
	public bool force = true;


	void Start (){
		thrust = multiplier * Time.timeSinceLevelLoad * Time.deltaTime;
		rb = transform.GetComponent<Rigidbody> ();
		PlayerCorpse();
	}

	private void PlayerCorpse () {
		rb.AddForce (0f, 0f, thrust, ForceMode.Impulse);
		force = false;
	}
}
