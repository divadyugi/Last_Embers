using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseScreen;
    private bool isPaused = false;
    public GameObject HUDCanvas;
    public GameObject controlCanvas;
    private bool inControls;


    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) ){
            if(!inControls){
                if(!isPaused){
                    pauseGame();
                }else if(isPaused){
                    continueFunction();
                }
            }
        }
    }

    public void pauseGame(){
        //player.GetComponent<Player>().enabled = false;
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        isPaused = true;
    }
    public void continueFunction(){
       // player.GetComponent<Player>().enabled = true;
       Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        isPaused = false;
            }

    public void menuFunction(){
        
        SceneManager.LoadScene("MainMenu");
    }

    public void controlFunction(){
        controlCanvas.SetActive(true);
        pauseScreen.SetActive(false);
        HUDCanvas.SetActive(false);
        inControls = true;
    }

    public void controlBack(){
        controlCanvas.SetActive(false);
        pauseScreen.SetActive(true);
        HUDCanvas.SetActive(true);
        inControls = false;
    }
}
