using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour {

    void Update()
    {
        PlayerDeathHotkeys();
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
    }

    public void RestartGame(string restartGame)
    {
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
                SceneManager.LoadScene("Tunnel");
                Cursor.visible = false;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                Debug.Log("Hit Esc");
            }
        }
    }

    #endregion
}
