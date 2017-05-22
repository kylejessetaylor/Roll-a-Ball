using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void PlayBtn (string startGame)
	{
		SceneManager.LoadScene (startGame);
	}

	public void ExitGameBtn () {
		Application.Quit ();
	}

	public void RestartGame (string restartGame) {
		SceneManager.LoadScene (restartGame);
	}
}
