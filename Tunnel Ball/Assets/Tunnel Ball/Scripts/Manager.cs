using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MenuButtons {

    [Header("Game Setup")]
	public GameObject firstTunnel;

	void Start () {
        //Builds First Tunnel
		BuildLevel (firstTunnel);

        //Loads in the current highscore into the game on launch
        currentHighScore = PlayerPrefs.GetFloat("Highscore");
        highScore.text = "Highscore:  " + PlayerPrefs.GetFloat("Highscore");
        stopScore = false;
    }

	void Update (){
        //Updates Score
        if (stopScore == false)
        {
            CurrentScore();

            //FPS
            FrameRate();
        }

        PlayerDeathHotkeys();
    }

    #region FrameRate

    [Header("FrameRate")]
    public Text frameRate;
    private float currentFPS;
    private float averageFPS;
    private float lowestFPS;

    private void FrameRate()
    {
        
    }


    #endregion

    #region FirstTunnel

    //Places Tunnel_001 on game start
    private void BuildLevel(GameObject tunnelPieceToPlace)
    {
        //GameObject newTunnel =
        TrashMan.spawn(tunnelPieceToPlace, (Vector3.forward * -0.24f), Quaternion.identity);
    }
    #endregion

    #region Score

    [Header("Score")]
    public Text score;
    public Text highScore;
    public Text finalScore;
    public Text whoops;

    public float scoremultiplier = 0.2f;
    protected float endScore;
    protected float scorez;
    protected float currentHighScore;

    private bool stopScore = false;

    public void CurrentScore()
    {
        //Stops score going up after death
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            //Score calculator
            scorez = scoremultiplier * Mathf.Pow(Time.timeSinceLevelLoad, 1.5f);
            //Score Text
            score.text = "Score:  " + Mathf.Round(scorez);
        }
        //Shows player's current score that round
        if (scorez <= currentHighScore && (GameObject.FindGameObjectWithTag("Player") == null))
        {
            YourScore();
        }
        //Saves new highscore and applies text when player dies
        if (scorez > currentHighScore && (GameObject.FindGameObjectWithTag("Player") == null))
        {
            SaveScore();
            NewHighScore();
        }
    }

    //Shows Score on Endgame screne
    public void YourScore()
    {
        finalScore.text = "Your Score: " + Mathf.Round(scorez);
        whoops.text = "Whoops!";

        //textChange [Random.Range (0, textList.Count)]

        //Stops numbers from continuing to calculate after death
        stopScore = true;
    }

    //Updates and Shows Highscore on Endgame screne
    public void NewHighScore()
    {
        finalScore.text = "Your Highscore:  " + Mathf.Round(scorez);
        whoops.text = "New Highscore!";

        //Loads the new highscore into the highscore text
        highScore.text = "Highscore:  " + ((int)PlayerPrefs.GetFloat("Highscore")).ToString();

        //Stops numbers from continuing to calculate after death
        //this.gameObject.GetComponent<Score>().enabled = false;
    }

    //Saves new Highscore
    public void SaveScore()
    {
        PlayerPrefs.SetFloat("Highscore", Mathf.Round(scorez));
    }

    //	private Text FunnyText (Text textChange) {
    //
    //	}

    #endregion
}
