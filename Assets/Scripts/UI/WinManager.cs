using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
public GameObject HUDCanvas;
    public GameObject deathScreen;
    //start up death manager canvas
    public void startWinScreen(){
        Time.timeScale = 0f;
        HUDCanvas.SetActive(false);
        deathScreen.SetActive(true);
    }

    //restart game
    public void restartGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        SceneManager.LoadScene("Backup.5");
    }

    //load main menu scene
    public void mainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
