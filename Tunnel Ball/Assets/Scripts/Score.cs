using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	//Score
	public float scoremultiplier = 0.2f;
	public Text score;
	public Text finalScore;
	public float timeBetweenScore = 2.0f;
	float scorez;

	//Player Rotation
	public float rotateSpeed = 30f;
	float tractionSpeed;


	// Use this for initialization
	void Start () {
		CurrentScore ();
	}
	
	// Update is called once per frame
	void Update () {
		CurrentScore ();
		MarbleRotation ();
	}

	public void CurrentScore () {
		scorez = scoremultiplier * Mathf.Pow (Time.timeSinceLevelLoad, 1.5f);
		score.text = "Score: " + Mathf.Round (scorez);
		finalScore.text = "Your Score: " + Mathf.Round (scorez);
	}
		
	//Marble rotation that increases on change in time.
	private void MarbleRotation() {
		tractionSpeed = rotateSpeed * Time.timeSinceLevelLoad;
		transform.Rotate (new Vector3 (tractionSpeed, 0, 0) * Time.deltaTime);
	}
}
