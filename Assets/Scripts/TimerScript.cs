using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;

public class TimerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int length;
    [SerializeField] private Text text;
    [SerializeField] Light2D light2D;
    public Player player;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "Time: "+length+" Seconds";
        StartCoroutine(timerCountDown());
    }

    IEnumerator timerCountDown(){
        while(true){
        yield return new WaitForSeconds(1);
        length--;
        if(length<=200){
        light2D.intensity-=0.00025f;
        }
        text.text = "Time: "+length+" Seconds";
        if(length<=0){
            player.win = true;
            yield break;
        }
        }
    }

    
}
