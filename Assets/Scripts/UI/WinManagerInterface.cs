using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManagerInterface : MonoBehaviour
{
    private WinManager winManager;

    void Start(){
        winManager = GameObject.Find("WinManager").GetComponent<WinManager>();
    }

    public void restart(){
        winManager.restartGame();
    }

    public void menu(){
        winManager.mainMenu();
    }
}
