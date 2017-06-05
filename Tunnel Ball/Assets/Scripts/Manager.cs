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
		//Resumes Game from "Pause" created by DeathTrigger.
		Time.timeScale = 1;
		//Reloads scene.
		SceneManager.LoadScene (restartGame);
		Cursor.visible = false;
	}

	//Spawns 1st tunnel on start
	public GameObject tunnelPrefab;

	void Start () {
//		GameObject tunnelPieceToPlace = null;
//
//		tunnelPieceToPlace = tunnel_001Prefab, [Random.Range (0, 1)];

		Instantiate (tunnelPrefab, (Vector3.forward * -0.24f), Quaternion.identity);
	}
}
