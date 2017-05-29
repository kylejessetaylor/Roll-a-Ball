using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public float scoremultiplier = 0.2f;
	public Text score;
	public Text finalScore;
	public float timeBetweenScore = 2.0f;

	float scorez;


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
		finalScore.text = "Your Score: " + Mathf.Round (scorez);
	}
}
