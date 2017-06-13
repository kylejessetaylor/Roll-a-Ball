using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTrigger : MonoBehaviour {

	public List<GameObject> tunnelPieceList = new List<GameObject> ();

	[HideInInspector]
	public GameObject tunnelPiece;
	//A counter for all the pieces spawned
	public int tunnelPieceCounter = 0;
	//Depth of each tunnel piece
	public int depthOfTunnelPiece = 200;
	//How many pieces we want to pull
	public int numberOfTunnelPieces = 2;

	public float playerPositionCounter = 0;
	private GameObject player;

	bool buildPossible;

	// Use this for initialization
	void Start () {
//		BuildLevel ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		if (buildPossible) {
			BuildLevel (tunnelPieceList [Random.Range (0, tunnelPieceList.Count)]);
			buildPossible = false;
		}
	}

	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			buildPossible = true;
		}
	}

	private void BuildLevel (GameObject tunnelPieceToPlace) {

		GameObject newTunnel = TrashMan.spawn (tunnelPieceToPlace, (Vector3.forward * depthOfTunnelPiece), Quaternion.identity);
	}
}
