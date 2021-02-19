using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public StartManager startManager;

    void Start(){
        startManager = GameObject.Find("StartManager").GetComponent<StartManager>();
    }

    public void startGameManage(){
        startManager.startGame();
    }
}
