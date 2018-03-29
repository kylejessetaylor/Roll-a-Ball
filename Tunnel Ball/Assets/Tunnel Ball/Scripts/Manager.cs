using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MenuButtons {

    [Header("Game Setup")]
	public GameObject firstTunnel;

	void Start () {
        //TEST DELETE ME
        //PlayerPrefs.SetFloat("Highscore", 0);
        //TEST DELETE ME

        //Sets old high score for number tick
        oldHighScore = PlayerPrefs.GetFloat("Highscore");
        numberTick = oldHighScore;
        //ButtonMask
        startHeight = 1f;

        //Builds First Tunnel
        BuildLevel (firstTunnel);

        //Loads in the current highscore into the game on launch
        currentHighScore = PlayerPrefs.GetFloat("Highscore");
        highScore.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore");

        //Fog
        //InvokeRepeating("FogUpdate", 1f, 1f);
    }

    void Update (){
        //Updates Score
        CurrentScore();            
        
        //For PC use
        PlayerDeathHotkeys();
    }

    #region Fog

    ////Fog
    //[Header("Fog")]
    //public float maxFog = 0.02f;
    //public float minFog = 0.015f;
    //public float fogRate = 1f;

    //void FogUpdate()
    //{
    //    ////Fog Density based on Velocity
    //    //RenderSettings.fogDensity = Mathf.Lerp(maxFog, minFog, fogRate * Time.deltaTime);
    //}

    //void FogFading()
    //{
    //    StartCoroutine("FogFadingCoroutine");
    //}

    //IEnumerable FogFadingCoroutine()
    //{
    //    do
    //    {
    //        RenderSettings.fogDensity -= fogRate * Time.deltaTime;
    //        return yield null;
    //    } while (RenderSettings.fogDensity > minFog);
    //}

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
    public float scoremultiplier = 0.475f;

    [Header("Score")]
    //In Game UI
    public GameObject scoreTable;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    private float oldHighScore;
    private float numberTick;
    private float lilNumber;

    [Header("Dead")]
    private float startHeight;
    public TextMeshProUGUI header;
    public GameObject tryAgain;

    public float scoreLerp = 3f;
    public float fadeInDelay = 5f;
    public float startTicking = 1.5f;
    public float numberTickRate = 0.1f;
    public float fontIncreaseRate = 0.1f;

    public GameObject leftButton;
    public GameObject rightButton;

    [Header("Dead Old")]
    ///Dead UI
    public GameObject deadTable;
    //For YourScore only
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI deadHighScore;
    //For NewHighScore only
    public TextMeshProUGUI newHighScore;
    public TextMeshProUGUI whoops;

    protected float endScore;
    protected float scorez;
    protected float currentHighScore;

    public void CurrentScore()
    {
        //If the player still exsists
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            //Score calculator
            scorez = scoremultiplier * Mathf.Pow(Time.timeSinceLevelLoad, 1.5f);
            //Score Text
            score.text = "Score: " + Mathf.Round(scorez);
        }
        //Shows player's current score that round
        else if (scorez <= currentHighScore)
        {
            YourScore();
        }
        //Saves new highscore and applies text when player dies
        else if (scorez > currentHighScore)
        {
            SaveScore();
            YourScore();
        }
    }

    private float freeMulti = 80f;

    //Shows Score on Endgame screen
    public void YourScore()
    {
        /// SMOOTH movement of current score to center
        /// --------------------------------------------------------
        //Turns Buttons off
        leftButton.SetActive(false);
        rightButton.SetActive(false);

        #region Bothscore Setup

        //Lerp Movement transition
        scoreTable.transform.position = Vector3.Lerp(scoreTable.transform.position, deadTable.transform.position, scoreLerp * Time.deltaTime);
        //Sprite change
        scoreTable.GetComponent<Image>().sprite = deadTable.GetComponent<Image>().sprite;
        //Lerp Size transition
        RectTransform scoreRect = scoreTable.GetComponent<RectTransform>();
        RectTransform deadRect = deadTable.GetComponent<RectTransform>();
        //Smooth RectTransform change from Score Size to Dead Size
        float changeX = Mathf.Lerp(scoreRect.sizeDelta.x, deadRect.sizeDelta.x, scoreLerp * Time.deltaTime);
        float changeY = Mathf.Lerp(scoreRect.sizeDelta.y, deadRect.sizeDelta.y, scoreLerp * Time.deltaTime);
        scoreRect.sizeDelta = new Vector2(changeX, changeY);

        //Highscore
        highScore.transform.localPosition = Vector3.Lerp(highScore.transform.localPosition, deadHighScore.transform.localPosition, scoreLerp * Time.deltaTime);
        highScore.GetComponent<TextMeshProUGUI>().fontSize = Mathf.Lerp(highScore.GetComponent<TextMeshProUGUI>().fontSize,
            deadHighScore.GetComponent<TextMeshProUGUI>().fontSize, scoreLerp * Time.deltaTime);

        //ButtonMask
        RectTransform buttonMask = tryAgain.transform.parent.GetComponent<RectTransform>();
        buttonMask.sizeDelta = new Vector2(buttonMask.sizeDelta.x, 300f);
        //freeMulti -= 22.5f * Time.deltaTime;
        //startHeight += scoreLerp * freeMulti * Time.deltaTime;
        ////Stops Mask's height inverting
        //if (buttonMask.sizeDelta.y <= 300f)
        //{
        //    buttonMask.sizeDelta = new Vector2(buttonMask.sizeDelta.x, startHeight);
        //}



        float buttonMultiplier = 1.5f;
        //Try Button Button
        tryAgain.gameObject.SetActive(true);
        Color tA = tryAgain.GetComponent<Image>().color;
        tA.a += (scoreLerp * buttonMultiplier) / fadeInDelay * Time.deltaTime;
        tryAgain.GetComponent<Image>().color = tA;
        //Try Button Text
        Color tAT = tryAgain.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        tAT.a += (scoreLerp * buttonMultiplier * 2f) / (fadeInDelay) * Time.deltaTime;
        tryAgain.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = tAT;

        #endregion

        #region Highscore Setup

        ///Highscore Setup
        if (scorez >= currentHighScore)
        {
            //Header
            header.gameObject.SetActive(true);
            Color h = header.GetComponent<TextMeshProUGUI>().color;
            h.a += (scoreLerp * 1.5f) / fadeInDelay * Time.deltaTime;
            header.GetComponent<TextMeshProUGUI>().color = h;

            //Score
            Color s = score.GetComponent<TextMeshProUGUI>().color;
            s.a -= scoreLerp / 4f * fadeInDelay * Time.deltaTime;
            score.GetComponent<TextMeshProUGUI>().color = s;

            //Checks for Screen animations to finish
            if (deadHighScore.GetComponent<TextMeshProUGUI>().fontSize -
            highScore.GetComponent<TextMeshProUGUI>().fontSize <= startTicking)
            {
                //Highscore Digit increase
                if (Mathf.Round(numberTick) < Mathf.Round(scorez))
                {
                    numberTick += numberTickRate * (scorez - oldHighScore) * Time.deltaTime;
                    highScore.text = "Highscore: " + Mathf.Round(numberTick);
                    
                    //Increases Font Size progressively
                    if (numberTick - lilNumber >= 1f)
                    {
                        lilNumber = numberTick;
                        highScore.fontSize += fontIncreaseRate;
                    }
                }
                //Stops going above actual highscore
                else
                {
                    highScore.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore");
                }
            }
            
        }
        #endregion

        #region No-Highscore Setup
        ///No-Highscore Setup
        else
        {
            //Score
            score.transform.localPosition = Vector3.Lerp(score.transform.localPosition, finalScore.transform.localPosition, scoreLerp * Time.deltaTime);
            score.GetComponent<TextMeshProUGUI>().fontSize = Mathf.Lerp(score.GetComponent<TextMeshProUGUI>().fontSize,
                finalScore.GetComponent<TextMeshProUGUI>().fontSize, scoreLerp * Time.deltaTime);
        }
        #endregion
       
    }

    //Saves new Highscore
    public void SaveScore()
    {
        PlayerPrefs.SetFloat("Highscore", Mathf.Round(scorez));
    }

    #endregion
}
