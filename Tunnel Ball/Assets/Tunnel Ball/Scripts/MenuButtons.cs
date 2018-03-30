using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour {

    public GameObject player;
    public GameObject uiPannel;
    public float fadeRate;

    [Header("Audio")]
    public GameObject audioManager;

    private bool gameStarted;

    void Update()
    {
        PlayerDeathHotkeys();
        //Fades Menu buttons
        if (gameStarted == true)
        {
            FadeButtons();
        }

        Time.timeScale = 1;
    }

    #region Buttons

    //Menu Buttons
    public void PlayBtn(string startGame)
    {
        //Loads a scene.
        SceneManager.LoadScene(startGame);
        Cursor.visible = false;
    }

    public void ExitGameBtn()
    {
        //Exits Game.
        Application.Quit();
        audioManager.GetComponent<AudioSource>().Play();
    }

    public void RestartGame(string restartGame)
    {
        //Audio Sound
        audioManager.GetComponent<AudioSource>().Play();

        //Reloads scene.
        StartCoroutine(ExecuteAfterTime(0.2f, restartGame));
    }

    IEnumerator ExecuteAfterTime(float time, string restartGame)
    {
        //suspend execution for X seconds
        yield return new WaitForSeconds(time);

        //Reloads scene.
        SceneManager.LoadScene(restartGame);
        Cursor.visible = false;
    }


    #endregion

    #region Hotkeys

    //Can use Enter & Esc to navigate menu
    public void PlayerDeathHotkeys()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                RestartGame("Tunnel");
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                Debug.Log("Hit Esc");
            }
        }
    }

    #endregion

    //Function for button to activate
    public void StartGame()
    {
        player.transform.GetComponent<MarbleSpiral>().startButtonPressed = true;
        gameStarted = true;
        audioManager.GetComponent<AudioSource>().Play();
    }

    //Fades out menu
    void FadeButtons()
    {
        //Button
        Color colorButton = uiPannel.transform.GetChild(0).GetComponent<Image>().color;
        colorButton.a -= fadeRate * Time.deltaTime;
        uiPannel.transform.GetChild(0).GetComponent<Image>().color = colorButton;

        //Button Text
        Color colorButtonText = uiPannel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color;
        colorButtonText.a -= fadeRate * Time.deltaTime;
        uiPannel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = colorButtonText;

        //Title
        Color colorTitle = uiPannel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color;
        colorTitle.a -= fadeRate * Time.deltaTime;
        uiPannel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = colorTitle;
    }
}
