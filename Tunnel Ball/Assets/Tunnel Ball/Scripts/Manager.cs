﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MenuButtons {

    [Header("Game Setup")]
	public GameObject firstTunnel;
    [HideInInspector]
    public GameObject playerC;

    public float mobileMultiplier = 1.25f;

    private bool firstTick;

    void Awake()
    {       
        //Sets player
        playerC = GameObject.FindGameObjectWithTag("Player");

        //Hides Pause Menu
        HidePauseOnStart();

        //Reduce gravity
        Physics.gravity = new Vector3(0f, -9.8f, 0f);

        //Audio
        rollingSound = GameObject.Find("MarbleRolling");
    }

    void Start () {
        //TEST DELETE ME
        //PlayerPrefs.SetFloat("Highscore", 0);
        //TEST DELETE ME

        //Audio ticking
        firstTick = true;

        //Disables restart button
        tryAgain.GetComponent<Button>().interactable = false;

        //Sets Android's max FPS
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Application.targetFrameRate = 45;
            Debug.Log("Mobile");
        }

        //Sets old high score for number tick
        oldHighScore = PlayerPrefs.GetFloat("Highscore");
        numberTick = oldHighScore;

        //Builds First Tunnel
        BuildLevel (firstTunnel);

        //Loads in the current highscore into the game on launch
        currentHighScore = PlayerPrefs.GetFloat("Highscore");
        highScore.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore");
    }

    void Update (){
        //Updates Score
        CurrentScore();            
        
        //For PC use
        PlayerDeathHotkeys();
        //Pause Hotkeys
        if (GameObject.FindGameObjectWithTag("Player") == true)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Space))
            {
                if (Time.timeScale == 1)
                {
                    Pause();
                }
                else
                {
                    UnPause();
                }
            }
        }

        //Pause Menu
        if (isPaused == true)
        {
            PauseActions();
        }

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            exitGame.SetActive(false);
        }
    }

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

    //Sound
    private GameObject rollingSound;

    //Calculates score during runtime & finalizes/saves it after death
    public void CurrentScore()
    {
        //If the player still exists
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

    //Shows Score on Endgame screen
    public void YourScore()
    {
        //Soften Music
        backgroundMusic.GetComponent<AudioSource>().volume -= backgroundMusic.GetComponent<AudioSource>().volume * Time.unscaledDeltaTime * 2f;
        //Soften Music
        rollingSound = GameObject.Find("MarbleRolling");
        rollingSound.GetComponent<AudioSource>().volume -= rollingSound.GetComponent<AudioSource>().volume * Time.unscaledDeltaTime * 2f;

        //Turns Buttons off
        leftButton.SetActive(false);
        rightButton.SetActive(false);

        //Disables Pause Button
        pauseButtonText.transform.parent.GetComponent<Button>().interactable = false;
        //Disables restart button
        tryAgain.GetComponent<Button>().interactable = true;
        //Exit Button interactable
        exitGame.GetComponent<Button>().interactable = true;

        #region Bothscore Setup

        //Lerp Movement transition
        scoreTable.transform.position = Vector2.Lerp(scoreTable.transform.position, deadTable.transform.position, scoreLerp * Time.unscaledDeltaTime);
        //Sprite change
        scoreTable.GetComponent<Image>().sprite = deadTable.GetComponent<Image>().sprite;
        //Lerp Size transition
        RectTransform scoreRect = scoreTable.GetComponent<RectTransform>();
        RectTransform deadRect = deadTable.GetComponent<RectTransform>();
        //Smooth RectTransform change from Score Size to Dead Size
        float changeX = Mathf.Lerp(scoreRect.sizeDelta.x, deadRect.sizeDelta.x, scoreLerp * Time.unscaledDeltaTime);
        float changeY = Mathf.Lerp(scoreRect.sizeDelta.y, deadRect.sizeDelta.y, scoreLerp * Time.unscaledDeltaTime);
        scoreRect.sizeDelta = new Vector2(changeX, changeY);

        //Highscore
        highScore.transform.localPosition = Vector2.Lerp(highScore.transform.localPosition, deadHighScore.transform.localPosition, scoreLerp * Time.unscaledDeltaTime);
        highScore.GetComponent<TextMeshProUGUI>().fontSize = Mathf.Lerp(highScore.GetComponent<TextMeshProUGUI>().fontSize,
            deadHighScore.GetComponent<TextMeshProUGUI>().fontSize, scoreLerp * Time.unscaledDeltaTime);

        //ButtonMask
        RectTransform buttonMask = tryAgain.transform.parent.GetComponent<RectTransform>();
        buttonMask.sizeDelta = new Vector2(buttonMask.sizeDelta.x, 300f);

        float buttonMultiplier = 1.5f;
        //Try Button Button
        tryAgain.gameObject.SetActive(true);
        Color tA = tryAgain.GetComponent<Image>().color;
        tA.a += (scoreLerp * buttonMultiplier) / fadeInDelay * Time.unscaledDeltaTime;
        tryAgain.GetComponent<Image>().color = tA;
        //Try Button Text
        Color tAT = tryAgain.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        tAT.a += (scoreLerp * buttonMultiplier * 2f) / (fadeInDelay) * Time.unscaledDeltaTime;
        tryAgain.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = tAT;

        ///Exit Game
        //if not using mobile
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            //Exit Button
            exitGame.gameObject.SetActive(true);
            Color eG = exitGame.GetComponent<Image>().color;
            eG.a += (scoreLerp * buttonMultiplier) / fadeInDelay * Time.unscaledDeltaTime;
            exitGame.GetComponent<Image>().color = eG;
            //Exit Button Text
            Color eGT = exitGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
            eGT.a += (scoreLerp * buttonMultiplier * 2f) / (fadeInDelay) * Time.unscaledDeltaTime;
            exitGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = eGT;
        }

#endregion

#region Highscore Setup

        ///Highscore Setup
        if (scorez >= currentHighScore)
        {
            //Header
            header.gameObject.SetActive(true);
            Color h = header.GetComponent<TextMeshProUGUI>().color;
            h.a += (scoreLerp * 1.5f) / fadeInDelay * Time.unscaledDeltaTime;
            header.GetComponent<TextMeshProUGUI>().color = h;

            //Score
            Color s = score.GetComponent<TextMeshProUGUI>().color;
            s.a -= scoreLerp / 4f * fadeInDelay * Time.unscaledDeltaTime;
            score.GetComponent<TextMeshProUGUI>().color = s;

            //Checks for Screen animations to finish
            if (deadHighScore.GetComponent<TextMeshProUGUI>().fontSize -
            highScore.GetComponent<TextMeshProUGUI>().fontSize <= startTicking)
            {
                //Audio Highscore tick
                AudioSource tickSound = audioManager.transform.GetChild(1).GetComponent<AudioSource>();

                //Highscore Digit increase
                if (Mathf.Round(numberTick) < Mathf.Round(scorez))
                {                   
                    //Increases temp number for score increase
                    numberTick += numberTickRate * (scorez - oldHighScore) * Time.unscaledDeltaTime;
                    highScore.text = "Highscore: " + Mathf.Round(numberTick);
                    
                    //First Tick
                    if (firstTick)
                    {
                        tickSound.Play();
                        firstTick = false;
                    }

                    //Increases Font Size progressively
                    if (numberTick - lilNumber >= 1f)
                    {
                        lilNumber = numberTick;
                        highScore.fontSize += fontIncreaseRate;

                        //Play Audio
                        if (tickSound.isPlaying == false)
                        {
                            tickSound.Play();
                            if (tickSound.pitch < 1f)
                            {
                                tickSound.pitch += numberTickRate * Time.unscaledDeltaTime;
                            }
                            else
                            {
                                tickSound.pitch = 1;
                            }
                        }
                    }
                }
                //Stops going above actual highscore
                else
                {
                    highScore.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore");

                    //Disables audio
                    tickSound.enabled = false;
                }
            }
            
        }
#endregion

#region No-Highscore Setup
        ///No-Highscore Setup
        else
        {
            //Score
            score.transform.localPosition = Vector3.Lerp(score.transform.localPosition, finalScore.transform.localPosition, scoreLerp * Time.unscaledDeltaTime);
            score.GetComponent<TextMeshProUGUI>().fontSize = Mathf.Lerp(score.GetComponent<TextMeshProUGUI>().fontSize,
                finalScore.GetComponent<TextMeshProUGUI>().fontSize, scoreLerp * Time.unscaledDeltaTime);
        }
#endregion

        LerpPauseButton(originalColor, transparent);     
    }

    //Saves new Highscore
    public void SaveScore()
    {
        PlayerPrefs.SetFloat("Highscore", Mathf.Round(scorez));
    }

#endregion

    #region Pause

    [Header("Pause Menu")]
    private bool isPaused = false;
    private bool unPause = false;

    public GameObject exitGame;

    public GameObject pauseButtonText;
    public GameObject pauseTitle;
    public GameObject resumeButton;
    public GameObject menuButton;
    public float countDownStartTime = 3.5f;

    private Color originalColor;
    public Color fadedColor;
    private Color transparent;

    private float countDown;

    //Input into Start Function
    void HidePauseOnStart()
    {
        isPaused = false;
        unPause = false;

        //Title enable
        pauseTitle.SetActive(false);

        ///Disable Resume Button ---------------------------------------------------------------
        resumeButton.GetComponent<Button>().interactable = false;

        Color rb = resumeButton.GetComponent<Image>().color;
        rb.a = 0;
        resumeButton.GetComponent<Image>().color = rb;
        //Fade-Out Resume Text
        Color rt = resumeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        rt.a = 0;
        resumeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = rt;


        ///Disable Menu Button -----------------------------------------------------------------
        menuButton.GetComponent<Button>().interactable = false;

        Color mb = menuButton.GetComponent<Image>().color;
        mb.a = 0;
        menuButton.GetComponent<Image>().color = mb;
        //Fade-Out Menu Text
        Color mt = menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        mt.a = 0;
        menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = mt;

        //Disables Buttons
        resumeButton.SetActive(false);
        menuButton.SetActive(false);

        //Color
        originalColor = pauseButtonText.GetComponent<TextMeshProUGUI>().color;
        transparent = new Color(1, 1, 1, 0);
    }

    //Pause Button
    public void Pause()
    {
        if (playerC)
        {
            isPaused = true;
            unPause = false;
            //Button Audio Sound
            audioManager.transform.GetChild(0).GetComponent<AudioSource>().Play();

            //Disable Control Buttons
            leftButton.SetActive(false);
            rightButton.SetActive(false);
        }
    }

    //Resume Button
    public void UnPause()
    {
        if (playerC.activeSelf)
        {
            unPause = true;
            //Button Audio Sound
            audioManager.transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
    }

    public void PauseActions()
    {
        ///Pauses Game -----------------------------------------------------------------------------------------------
        if (unPause == false)
        {
            //Disables exit game button
            exitGame.GetComponent<Button>().interactable = true;
            //Hotkey
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitGameBtn();
            }

            //Enables cursor
            Cursor.visible = true;

            //Pauses Game
            Time.timeScale = 0;
            countDown = countDownStartTime;

            //Dims Pause button's color
            LerpPauseButton(originalColor, fadedColor);

            //Title enable
            pauseTitle.GetComponent<Animator>().enabled = true;
            pauseTitle.SetActive(true);
            TextMeshProUGUI pauseText = pauseTitle.GetComponent<TextMeshProUGUI>();
            pauseText.text = "Paused";

            //Enable Resume Button
            resumeButton.SetActive(true);
            resumeButton.GetComponent<Button>().interactable = true;

            //ID button colors
            Color rb = resumeButton.GetComponent<Image>().color;
            Color rt = resumeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
            Color mb = menuButton.GetComponent<Image>().color;
            Color mt = menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;

            if (rb.a < 1 && mb.a < 1)
            {
                rb.a += (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime;
                resumeButton.GetComponent<Image>().color = rb;
                //Fade-In Resume Text
                rt.a += (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime;
                resumeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = rt;

                //Enable Menu Button
                menuButton.SetActive(true);
                menuButton.GetComponent<Button>().interactable = true;

                mb.a += (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime;
                menuButton.GetComponent<Image>().color = mb;
                //Fade-In Menu Text
                mt.a += (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime;
                menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = mt;

                //Exit Button
                if (SystemInfo.deviceType == DeviceType.Desktop)
                {
                    //Fade-In Exit Button
                    Color eG = exitGame.GetComponent<Image>().color;
                    eG.a += (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime;
                    exitGame.GetComponent<Image>().color = eG;
                    //Fade-In Exit Text
                    Color eGT = exitGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
                    eGT.a += (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime;
                    exitGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = eGT;
                }
            }

        }

        ///Un-pauses Game -----------------------------------------------------------------------------------------------
        else if (unPause)
        {
            //Disables exit game button
            exitGame.GetComponent<Button>().interactable = false;

            Color rb = resumeButton.GetComponent<Image>().color;
            Color rt = resumeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
            Color mb = menuButton.GetComponent<Image>().color;
            Color mt = menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;

            if (mt.a >= 0 && rt.a >= 0)
            {
                ///Disable Resume Button ---------------------------------------------------------------
                resumeButton.GetComponent<Button>().interactable = false;

                rb.a -= (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime * 2f;
                resumeButton.GetComponent<Image>().color = rb;
                //Fade-Out Resume Text
                rt.a -= (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime * 2f;
                resumeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = rt;

                ///Disable Menu Button -----------------------------------------------------------------
                menuButton.GetComponent<Button>().interactable = false;

                mb.a -= (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime * 2f;
                menuButton.GetComponent<Image>().color = mb;
                //Fade-Out Menu Text
                mt.a -= (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime * 2f;
                menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = mt;

                //Exit Button
                if (SystemInfo.deviceType == DeviceType.Desktop)
                {
                    //Fade-In Exit Button
                    Color eG = exitGame.GetComponent<Image>().color;
                    eG.a -= (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime;
                    exitGame.GetComponent<Image>().color = eG;
                    //Fade-In Exit Text
                    Color eGT = exitGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
                    eGT.a -= (scoreLerp * 2f) / fadeInDelay * Time.unscaledDeltaTime;
                    exitGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = eGT;
                }
            }

            //When both invisible, disable both buttons
            else
            {
                resumeButton.SetActive(false);
                menuButton.SetActive(false);

                ///Stops Alphas falling below 0
                //Resume Button/Text
                rb.a = 0;
                resumeButton.GetComponent<Image>().color = rb;
                rt.a = 0;
                resumeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = rt;
                //Menu Button/Text
                mb.a = 0;
                menuButton.GetComponent<Image>().color = mb;
                mt.a = 0;
                menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = mt;

                //Exit Button
                Color eG = exitGame.GetComponent<Image>().color;
                eG.a = 0;
                exitGame.GetComponent<Image>().color = eG;
                //Exit Text
                Color eGT = exitGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
                eGT.a = 0;
                exitGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = eGT;
            }

            ///Title enable -------------------------------------------------------------------------
            //Transform position to center
            pauseTitle.GetComponent<Animator>().enabled = false;
            pauseTitle.transform.position = Vector2.Lerp(pauseTitle.transform.position, resumeButton.transform.position, scoreLerp * Time.unscaledDeltaTime);
            //Lerps rotation from current to Vector3.zero (quaternion.identity)
            pauseTitle.transform.rotation = Quaternion.Lerp(pauseTitle.transform.rotation, Quaternion.identity, scoreLerp * Time.unscaledDeltaTime);

            //Enables cursor
            Cursor.visible = false;

            //-------------------------------------------------------------------------

            TextMeshProUGUI pauseText = pauseTitle.GetComponent<TextMeshProUGUI>();

            //Starts countdown if text is close to center
            if (Vector2.Distance(pauseTitle.transform.localPosition, resumeButton.transform.localPosition) <= 5f)
            {
                //Calculates countdown time
                countDown -= (scoreLerp * Time.unscaledDeltaTime) / 3f;
            }

            //Applies Number to Text
            if (countDown < countDownStartTime && Mathf.Round(countDown) > 0)
            {
                pauseText.text = Mathf.Round(countDown).ToString();
            }
            //Un-pauses Game
            else if (Mathf.Round(countDown) == 0)
            {
                //Stops game from re-pausing
                isPaused = false;
                unPause = false;

                //Reset Title
                pauseTitle.SetActive(false);
                pauseText.text = "Paused";

                //Pause Button Color
                LerpPauseButton(pauseButtonText.GetComponent<TextMeshProUGUI>().color, originalColor);

                //Enable Control Buttons
                leftButton.SetActive(true);
                rightButton.SetActive(true);

                //Un-pauses Game
                Time.timeScale = 1;
            }
            //Text to display b4 countdown
            else
            {
                pauseText.text = "Starting in";
            }
        }
    }

    public void LerpPauseButton(Color from, Color to)
    {
        TextMeshProUGUI pbText = pauseButtonText.GetComponent<TextMeshProUGUI>();
        Color color = pbText.color;
        //Color Lerp
        pbText.color = Color.Lerp(from, to, scoreLerp);
    }

#endregion
}
