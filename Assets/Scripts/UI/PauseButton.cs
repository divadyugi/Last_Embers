using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private PauseManager pauseManager;
    
    void Start() {
        pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();    
    }

    public void Continue(){
        pauseManager.continueFunction();
    }

    public void menu(){
        pauseManager.menuFunction();
    }

    public void controls(){
        pauseManager.controlFunction();
    }

    public void controlBack(){
        pauseManager.controlBack();
    }
}
