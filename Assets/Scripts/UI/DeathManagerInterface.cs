using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManagerInterface : MonoBehaviour
{
    private DeathManager deathManager;

    void Start(){
        deathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();
    }

    public void restart(){
        deathManager.restartGame();
    }

    public void menu(){
        deathManager.mainMenu();
    }
}
