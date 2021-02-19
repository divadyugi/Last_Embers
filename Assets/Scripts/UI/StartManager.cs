using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    public GameObject canvas;
    public Player player;

    void Awake() {
        canvas.SetActive(true);
        player.enabled = false;
        Time.timeScale = 0f;
    }

    public void startGame(){
        player.enabled = true;
        Time.timeScale = 1f;
        canvas.SetActive(false);
    }
}
