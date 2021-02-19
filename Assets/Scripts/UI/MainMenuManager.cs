using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    //Change scene to game scene
    public GameObject HUDCanvas;
    public GameObject controlCanvas;
    public void startGame(){
        SceneManager.LoadScene("Backup.5");
    }


    //Change scene to credits scene
    public void startCredits(){
        SceneManager.LoadScene("Credits");
    }

    public void startCreditsBack(){
        SceneManager.LoadScene("MainMenu");
    }

    //Enable The controls canvas
    
    public void controlFunction(){
        controlCanvas.SetActive(true);
        HUDCanvas.SetActive(false);
    }

    public void controlBack(){
        controlCanvas.SetActive(false);
        HUDCanvas.SetActive(true);

    }
    
}
