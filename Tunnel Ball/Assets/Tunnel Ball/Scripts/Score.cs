using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text score;
	public Text highScore;
	public Text finalScore;
	public Text whoops;

	public float scoremultiplier = 0.2f;
	protected float endScore;
	protected float scorez;
	protected float currentHighScore;

	
	void Start () {
		//Loads in the current highscore into the game on launch
		currentHighScore = PlayerPrefs.GetFloat ("Highscore");
		highScore.text = "Highscore: " + PlayerPrefs.GetFloat ("Highscore");
	}

	// Update is called once per frame
	void Update () {
		CurrentScore ();
	}

	public void CurrentScore () {
		//Score calculator
		scorez = scoremultiplier * Mathf.Pow (Time.timeSinceLevelLoad, 1.5f);
		score.text = "Score: " + Mathf.Round (scorez);

		//Shows player's current score that round
		if (scorez <= currentHighScore && (GameObject.FindGameObjectWithTag ("Player") == null)) {
			YourScore ();
		}
		//Saves new highscore and applies text when player dies
		if (scorez > currentHighScore && (GameObject.FindGameObjectWithTag ("Player") == null)) {
			SaveScore ();
			NewHighScore ();
		}
	}

	//Shows Score on Endgame screne
	public void YourScore () {
		finalScore.text = "Your Score: " + Mathf.Round (scorez);
		whoops.text = "Whoops!";

		//textChange [Random.Range (0, textList.Count)]

		//Stops numbers from continuing to calculate after death
		this.gameObject.GetComponent<Score> ().enabled = false;
	}

	//Updates and Shows Highscore on Endgame screne
	public void NewHighScore () {
		finalScore.text = "Your Highscore: " + Mathf.Round (scorez);
		whoops.text = "New Highscore!";

		//Loads the new highscore into the highscore text
		highScore.text = "Highscore: " + ((int)PlayerPrefs.GetFloat ("Highscore")).ToString();

		//Stops numbers from continuing to calculate after death
		this.gameObject.GetComponent<Score> ().enabled = false;
	}

	//Saves new Highscore
	public void SaveScore () {
		PlayerPrefs.SetFloat ("Highscore", Mathf.Round (scorez));
	}

//	private void FunnyText (Text textChange) {
//
//	}
}