using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MenuButtons {

    [Header("Game Setup")]
	public GameObject firstTunnel;

	void Start () {
        //TEST DELETE ME
       // PlayerPrefs.SetFloat("Highscore", 0);
        //TEST DELETE ME

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
    //In Game UI
    public Text score;
    public Text highScore;

    ///Dead UI
    //For YourScore only
    public Text finalScore;
    public Text deadHighScore;
    //For NewHighScore only
    public Text newHighScore;

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

    //Shows Score on Endgame screen
    public void YourScore()
    {
        //Enalbes & Disables appropriate text objects
        newHighScore.transform.gameObject.SetActive(false);

        finalScore.transform.gameObject.SetActive(true);
        deadHighScore.transform.gameObject.SetActive(true);

        whoops.text = "";
        //Applies score to text
        finalScore.text = "Your Score: " + Mathf.Round(scorez);
        deadHighScore.text = "Highscore: " + ((int)PlayerPrefs.GetFloat("Highscore")).ToString();

        //textChange [Random.Range (0, textList.Count)]

        //Stops numbers from continuing to calculate after death
        stopScore = true;
    }

    //Updates and Shows Highscore on Endgame screen
    public void NewHighScore()
    {
        //Enalbes & Disables appropriate text objects
        finalScore.transform.gameObject.SetActive(false);
        deadHighScore.transform.gameObject.SetActive(false);

        newHighScore.transform.gameObject.SetActive(true);

        whoops.text = "Highscore!";
        ////Applies score to text
        newHighScore.text = "Your Highscore: " + Mathf.Round(scorez);
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
