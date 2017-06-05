using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public float scoremultiplier = 0.2f;
	public Text score;
	public Text finalScore;
	public Text highScore;
	public float timeBetweenScore = 2.0f;

	protected float scorez;
	protected float currentHighScore = 0;


	// Use this for initialization
	void Start () {
		CurrentScore ();
	}
	
	// Update is called once per frame
	void Update () {
		CurrentScore ();
	}

	public void CurrentScore () {
		scorez = scoremultiplier * Mathf.Pow (Time.timeSinceLevelLoad, 1.5f);
		score.text = "Score: " + Mathf.Round (scorez);
		if (scorez < currentHighScore) {
			finalScore.text = "Your Score: " + Mathf.Round (scorez);
		} else {
			finalScore.text = "New Highscore: " + Mathf.Round (scorez)+"!!";
		}
	}
}