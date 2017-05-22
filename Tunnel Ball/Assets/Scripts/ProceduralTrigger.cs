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
	public int depthOfTunnelPiece = 195;
	//How many pieces we want to pull
	public int numberOfTunnelPieces = 2;

	public float playerPositionCounter = 0;
	public GameObject player;

	bool yourVar;

	// Use this for initialization
	void Start () {
//		BuildLevel ();
	}

	void Update () {
		//if (yourVar) {
			//BuildLevel ();
			//yourVar = false;
		//}
	}

	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			//yourVar = true;
			BuildLevel();
		}
	}

	private void BuildLevel () {
		GameObject tunnelPieceToPlace = null;

		tunnelPieceToPlace = tunnelPieceList [Random.Range (0, tunnelPieceList.Count)];

		Instantiate (tunnelPieceToPlace, (Vector3.forward * depthOfTunnelPiece), Quaternion.identity);
	}
}
