using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManagerInterface : MonoBehaviour
{
    private MainMenuManager mainMenuManager;


    void Awake() {
        Time.timeScale = 1f;    
    }

    void Start() {
        mainMenuManager = GameObject.Find("MainMenuManager").GetComponent<MainMenuManager>();
    }

    public void Game(){
        mainMenuManager.startGame();
    }

    public void credit(){
        mainMenuManager.startCredits();
    }

    public void leaveCredit(){
        mainMenuManager.startCreditsBack();
    }

    public void startControl(){
        mainMenuManager.controlFunction();
    }

    public void backControl(){
        mainMenuManager.controlBack();
    }

}
