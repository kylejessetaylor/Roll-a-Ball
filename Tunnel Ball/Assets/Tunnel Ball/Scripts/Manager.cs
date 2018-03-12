using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	//Menu Buttons
	public void PlayBtn (string startGame) {
		//Loads a scene.
		SceneManager.LoadScene (startGame);
		Cursor.visible = false;
	}

	public void ExitGameBtn () {
		//Exits Game.
		Application.Quit ();
	}

	public void RestartGame (string restartGame) {
		//Reloads scene.
		SceneManager.LoadScene (restartGame);
		Cursor.visible = false;
	}

	private void StartExitButtons () {
		if (GameObject.FindGameObjectWithTag ("Player") == null) {
			if (Input.GetKeyDown (KeyCode.Return)) {
				SceneManager.LoadScene ("Tunnel");
				Cursor.visible = false;
			}
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.Quit ();
			}
		}
	}


	public GameObject tunnelPrefab;

	void Start () {
		BuildLevel (tunnelPrefab);
	}

	//Places Tunnel_001 on game start
	private void BuildLevel (GameObject tunnelPieceToPlace) {
		GameObject newTunnel = TrashMan.spawn (tunnelPieceToPlace, (Vector3.forward * -0.24f), Quaternion.identity);
	}

	void Update (){
		StartExitButtons ();
	}
}
